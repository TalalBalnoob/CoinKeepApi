using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using CoinKeep.Api.Extensions;
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
	[HttpGet("profile")]
	public IActionResult GetUser() {
		var userId = User.GetUserId();

		var userFromDb = db.Users.Include(u => u.Account).FirstOrDefault(u => u.Id == userId);
		if (userFromDb == null) return this.NotFound();

		var userProfile = new UserProfileDto {
			Id = userFromDb.Id,
			Email = userFromDb.Email,
			Username = userFromDb.Username,
			Account = new ReturnedAccountDTO {
				Id = userFromDb.Account?.Id ?? 0,
				Name = userFromDb.Account?.Name ?? string.Empty,
				Balance = userFromDb.Account?.Balance ?? 0,
			}
		};

		return this.Ok(userProfile);
	}

	[HttpPost("register")]
	public IActionResult SingUp(NewUserDto userDto) {
		User? isExist = db.Users.FirstOrDefault(u => userDto.Email == userDto.Email || u.Username == userDto.Username);
		if (isExist != null) return this.BadRequest("User with that email/username already exists");

		var hasher = new PasswordHasher<string>();

		var newUser = new User {
			Email = userDto.Email,
			Username = userDto.Username,
			Password = hasher.HashPassword(userDto.Username, userDto.Password),
			Id = 0,
			CreatedAt = DateTime.UtcNow,
			Account = new Account {
				Id = 0,
				Name = "Cash",
				CreatedAt = DateTime.UtcNow,
				Balance = userDto.initialBalance,
			}
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
			expires: DateTime.UtcNow.AddDays(100),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}

