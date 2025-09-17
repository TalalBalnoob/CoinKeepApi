using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using CoinKeep.Core.DTOs;
using CoinKeep.Core.Models;
using CoinKeep.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CoinKeep.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(AppDbContext db, IConfiguration config) : Controller {

	[Authorize]
	[HttpGet("getUsers")]
	public IActionResult GetUser() {
		var users = db.Users.Select(u => new UserProfileDto {
			Id = u.Id,
			Email = u.Email,
			Username = u.Username
		});

		return this.Ok(users);
	}

	[Authorize]
	[HttpPatch("balanceAdjustment")]
	public IActionResult BalanceAdjustment([FromBody] decimal amount) {
		var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
		if (userIdClaim == null) return Unauthorized();
		var userId = int.Parse(userIdClaim.Value);

		var userFromDb = db.Users.Find(userId);
		if (userFromDb == null) return NotFound();

		userFromDb.Balance = amount;
		db.Transactions.Add(new Transaction {
			Amount = amount - userFromDb.Balance,
			CategoryId = 0, // Balance Adjustment Category
			Note = "Balance Adjustment",
			Id = 0,
			UserId = userId,
			CreatedAt = DateTime.UtcNow,
		});

		db.Users.Update(userFromDb);
		db.SaveChanges();

		return this.Ok(new { newBalance = userFromDb.Balance });

	}

	[HttpPost("register")]
	public IActionResult SingUp(User user) {
		User? isExist = db.Users.FirstOrDefault(u => user.Email == user.Email || u.Username == user.Username);
		if (isExist != null) return this.BadRequest("User with that email/username already exists");

		var hasher = new PasswordHasher<string>();

		var newUser = new User {
			Email = user.Email,
			Username = user.Username,
			Password = hasher.HashPassword(user.Username, user.Password),
			Id = 0,
			CreatedAt = DateTime.UtcNow
		};

		db.Users.Add(newUser);
		db.SaveChanges();
		return this.Ok(new UserProfileDto {
			Id = newUser.Id,
			Username = newUser.Username,
			Email = newUser.Email,
		});
	}

	[HttpPost("login")]
	public IActionResult Login(LoginUserDto loginInfo) {
		User? userFromDb = db.Users.FirstOrDefault(u => u.Username == loginInfo.username);
		if (userFromDb == null) return this.NotFound();
		var hasher = new PasswordHasher<string>();

		var isValidPassword = hasher.VerifyHashedPassword(userFromDb.Username, userFromDb.Password, loginInfo.password);
		if (isValidPassword != PasswordVerificationResult.Success) return this.Unauthorized();

		var userToken = this.GenerateJwtToken(userFromDb);
		return this.Ok(userToken);
	}

	private string GenerateJwtToken(User user) {
		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.Email, user.Email),
		};

		var key = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(config["Jwt:Key"]!)
		);
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: config["Jwt:Issuer"],
			audience: config["Jwt:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddHours(8),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}

