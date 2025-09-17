using System.Security.Claims;

using CoinKeep.Api.Extensions;
using CoinKeep.Core.DTOs;
using CoinKeep.Core.Models;
using CoinKeep.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoinKeep.Api.Controllers {
	[Authorize]
	[Route("[controller]")]
	[ApiController]
	public class TransactionController(ILogger<TransactionController> logger, AppDbContext db) : ControllerBase {
		[HttpGet("{accountId}")]
		public IActionResult GetAllTransactions(int accountId) {
			var userId = User.GetUserId();

			var accountFromDb = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (accountFromDb != null) return NotFound("Account dose not exist");

			var transactions = db.Transactions.Where(u => u.AccountId == accountId).ToList();

			return Ok(transactions);
		}

		[HttpGet("{accountId}/{id}")]
		public IActionResult GetTransaction(int accountId, int id) {
			var userId = User.GetUserId();

			var accountFromDb = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (accountFromDb != null) return NotFound("Account dose not exist");

			var transaction = db.Transactions.FirstOrDefault(u => u.Id == id);

			if (transaction == null || transaction.AccountId != accountId)
				return NotFound();

			return Ok(transaction);
		}

		[HttpPost("{accountId}")]
		public IActionResult CreateTransaction(int accountId, [FromBody] TransactionDto transactionDto) {
			var userId = User.GetUserId();

			var accountFromDb = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (accountFromDb != null) return NotFound("Account dose not exist");

			var isCategoryExist = db.Categories.Any(c => c.Id == transactionDto.CategoryId && (c.UserId == userId || c.UserId == null));
			if (!isCategoryExist) return NotFound("Category dose not exist");

			var newTransaction = new Transaction {
				Id = 0,
				Note = transactionDto.Note ?? "",
				Amount = transactionDto.Amount,
				CategoryId = transactionDto.CategoryId,
				AccountId = accountId,
				CreatedAt = DateTime.UtcNow
			};

			accountFromDb.Balance += transactionDto.Amount;

			db.Transactions.Add(newTransaction);
			logger.LogInformation("New Transaction Has been created");
			db.SaveChanges();

			return Created();
		}


		[HttpPut("{accountId}/{id}")]
		public IActionResult UpdateTransaction(int accountId, int id, [FromBody] TransactionDto transactionDto) {
			var userId = User.GetUserId();
			var accountFromDb = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (accountFromDb != null) return NotFound("Account dose not exist");

			var transactionFromDB = db.Transactions.Find(id);
			if (transactionFromDB == null || transactionFromDB.AccountId != accountId) return NotFound("Transaction Not Found");

			var isCategoryExist = db.Categories.Any(c => c.Id == transactionDto.CategoryId && (c.UserId == userId || c.UserId == null));
			if (!isCategoryExist) return NotFound("Category dose not exist");

			transactionFromDB.Amount = transactionDto.Amount;
			transactionFromDB.Note = transactionDto.Note;
			transactionFromDB.CategoryId = transactionDto.CategoryId;

			logger.LogInformation("transaction {} has been updated", id);
			db.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{accountId}/{id}")]
		public IActionResult DeleteTransaction(int accountId, int id) {
			var userId = User.GetUserId();
			var accountFromDb = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (accountFromDb != null) return NotFound("Account dose not exist");

			var transactionFromDB = db.Transactions.Find(id);
			if (transactionFromDB == null || transactionFromDB.AccountId != accountId) return NotFound("Transaction Not Found");

			db.Transactions.Remove(transactionFromDB);
			logger.LogInformation("transaction {} has been deleted", id);
			db.SaveChanges();
			return NoContent();
		}
	}
}
