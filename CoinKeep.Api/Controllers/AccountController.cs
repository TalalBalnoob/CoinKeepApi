using CoinKeep.Api.Extensions;
using CoinKeep.Core.DTOs;
using CoinKeep.Core.Models;
using CoinKeep.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoinKeep.Api.Controllers {
	[Authorize]
	[Route("User/[controller]")]
	[ApiController]
	public class AccountController(AppDbContext db) : ControllerBase {

		[HttpGet]
		public IActionResult GetAllAccounts() {
			var userId = User.GetUserId();
			var accounts = db.Accounts.Where(a => a.UserId == userId).Select(a => new {
				a.Id,
				a.Name,
				a.Balance
			}).ToList();

			return this.Ok(accounts);
		}

		[HttpGet("{accountId}")]
		public IActionResult GetAccountInfo(int accountId) {
			var userId = User.GetUserId();
			var account = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (account == null) return NotFound("Account dose not exist");

			return this.Ok(new {
				account.Id,
				account.Name,
				account.Balance,
				account.CreatedAt
			});
		}

		[HttpPut("{accountId}/balanceAdjustment")]
		public IActionResult BalanceAdjustment(int accountId, BalanceAdjustmentDto balanceDto) {
			var userId = User.GetUserId();
			var account = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (account == null) return NotFound("Account dose not exist");

			var userFromDb = db.Users.Find(userId);
			if (userFromDb == null) return NotFound();

			db.Transactions.Add(new Transaction {
				Amount = balanceDto.Amount - account.Balance,
				CategoryId = 1, // Balance Adjustment Category
				Note = "Balance Adjustment",
				Id = 0,
				AccountId = accountId,
				CreatedAt = DateTime.UtcNow,
			});

			account.Balance = balanceDto.Amount;

			db.Accounts.Update(account);
			db.SaveChanges();

			return this.Ok(new { newBalance = account.Balance });

		}
	}
}
