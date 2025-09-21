using System.Security.Claims;

using CoinKeep.Api.Extensions;
using CoinKeep.Core.DTOs;
using CoinKeep.Core.Models;
using CoinKeep.Core.Statics;
using CoinKeep.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoinKeep.Api.Controllers {
	[Authorize]
	[Route("[controller]")]
	[ApiController]
	public class TransactionController(ILogger<TransactionController> logger, AppDbContext db) : ControllerBase {
		[HttpGet("{accountId}")]
		public IActionResult GetAllTransactions(int accountId) {
			var userId = User.GetUserId();

			var accountFromDb = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (accountFromDb == null) return NotFound("Account dose not exist");

			List<ReturnedTransactionDTO>? transactions = db.Transactions.Where(u => u.AccountId == accountId).Select(u => new ReturnedTransactionDTO {
				Id = u.Id,
				Amount = u.Amount,
				Note = u.Note,
				CategoryId = u.CategoryId,
				CreatedAt = u.CreatedAt
			}).ToList();

			return Ok(transactions);
		}

		[HttpGet("{accountId}/recent")]
		public IActionResult GetRecentTransactions(int accountId, [FromQuery] int limit = 10) {
			var userId = User.GetUserId();

			var accountFromDb = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (accountFromDb == null) return NotFound("Account dose not exist");

			if (limit < 0 || limit > 100) limit = 10;

			List<ReturnedTransactionDTO>? transactions = db.Transactions
															.Where(u => u.AccountId == accountId)
															.Include(t => t.Category)
															.OrderByDescending(t => t.CreatedAt)
															.Take(limit)
															.Select(u => new ReturnedTransactionDTO {
																Id = u.Id,
																Amount = u.Amount,
																Note = u.Note,
																CategoryId = u.CategoryId,
																CategoryName = u.Category != null ? u.Category.Name : null,
																transactionType = u.Category != null ? CategoryTypeExtensions.ToFriendlyString(u.Category.Type) : null,
																CreatedAt = u.CreatedAt
															}).ToList();

			return Ok(transactions);
		}

		[HttpGet("{accountId}/{id}")]
		public IActionResult GetTransaction(int accountId, int id) {
			var userId = User.GetUserId();

			var accountFromDb = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (accountFromDb == null) return NotFound("Account dose not exist");

			Transaction? transaction = db.Transactions.FirstOrDefault(u => u.Id == id);

			if (transaction == null || transaction.AccountId != accountId)
				return NotFound();

			return Ok(transaction);
		}

		[HttpPost("{accountId}")]
		public IActionResult CreateTransaction(int accountId, [FromBody] NewTransactionDto transactionDto) {
			var userId = User.GetUserId();

			var accountFromDb = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (accountFromDb == null) return NotFound("Account dose not exist");

			var categoryFromDb = db.Categories.FirstOrDefault(c => c.Id == transactionDto.CategoryId && (c.UserId == userId || c.UserId == null));
			if (categoryFromDb == null) return NotFound("Category dose not exist");

			var newTransaction = new Transaction {
				Id = 0,
				Note = transactionDto.Note ?? "",
				Amount = transactionDto.Amount,
				CategoryId = categoryFromDb.Id,
				Category = categoryFromDb,
				AccountId = accountId,
				Account = accountFromDb,
				CreatedAt = DateTime.UtcNow
			};

			if (categoryFromDb.Type == Core.Statics.CategoryType.Expense)
				accountFromDb.Balance -= transactionDto.Amount;
			else
				accountFromDb.Balance += transactionDto.Amount;


			db.Transactions.Add(newTransaction);
			logger.LogInformation("New Transaction Has been created");
			db.SaveChanges();

			return Created();
		}


		[HttpPut("{accountId}/{id}")]
		public IActionResult UpdateTransaction(int accountId, int id, [FromBody] NewTransactionDto transactionDto) {
			var userId = User.GetUserId();
			var accountFromDb = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (accountFromDb == null) return NotFound("Account dose not exist");

			var transactionFromDB = db.Transactions.Find(id);
			if (transactionFromDB == null || transactionFromDB.AccountId != accountId) return NotFound("Transaction Not Found");

			var categoryFromDb = db.Categories.FirstOrDefault(c => c.Id == transactionDto.CategoryId && (c.UserId == userId || c.UserId == null));
			if (categoryFromDb == null) return NotFound("Category dose not exist");

			decimal defAmount = transactionFromDB.Amount - transactionDto.Amount;

			// Update account balance based on category type
			if (categoryFromDb.Type == Core.Statics.CategoryType.Expense)
				accountFromDb.Balance += defAmount;
			else
				accountFromDb.Balance -= defAmount;

			transactionFromDB.Amount = transactionDto.Amount;
			transactionFromDB.Note = transactionDto.Note;
			transactionFromDB.CategoryId = categoryFromDb.Id;


			logger.LogInformation("transaction {} has been updated", id);
			db.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{accountId}/{id}")]
		public IActionResult DeleteTransaction(int accountId, int id) {
			var userId = User.GetUserId();
			var accountFromDb = db.Accounts.FirstOrDefault(a => a.Id == accountId && a.UserId == userId);
			if (accountFromDb == null) return NotFound("Account dose not exist");

			var transactionFromDB = db.Transactions.Include(t => t.Category).FirstOrDefault(t => t.Id == id);
			if (transactionFromDB == null || transactionFromDB.AccountId != accountId) return NotFound("Transaction Not Found");


			if (transactionFromDB.Category.Type == CategoryType.Expense)
				accountFromDb.Balance += transactionFromDB.Amount;
			else
				accountFromDb.Balance -= transactionFromDB.Amount;


			db.Transactions.Remove(transactionFromDB);
			logger.LogInformation("transaction {} has been deleted", id);
			db.SaveChanges();
			return NoContent();
		}
	}
}
