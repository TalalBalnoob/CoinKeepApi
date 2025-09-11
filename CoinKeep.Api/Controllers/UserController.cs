using CoinKeep.Core.DTOs;
using CoinKeep.Core.Models;
using CoinKeep.Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoinKeep.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller {
	private readonly AppDbContext _db;

	public UserController(AppDbContext db) {
		_db = db;
	}

	[HttpGet("profile")]
	public IActionResult getUser() {
		var users = _db.Users.Select(u => new UserProfileDto {
			Id = u.Id,
			Email = u.Email,
			Username = u.Username
		});

		return Ok(users);
	}

}
