using System.Security.Claims;

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
		[HttpGet]
		public IActionResult GetAllTransactions() {
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
				return Unauthorized();

			int userId = int.Parse(userIdClaim.Value);

			var transactions = db.Transactions.Where(u => u.UserId == userId).ToList();

			return Ok(transactions);
		}

		[HttpGet("{id}")]
		public IActionResult GetTransaction(int id) {
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
				return Unauthorized();

			int userId = int.Parse(userIdClaim.Value);

			var transaction = db.Transactions.FirstOrDefault(u => u.Id == id);

			if (transaction == null || transaction.UserId != userId)
				return NotFound();

			return Ok(transaction);
		}

		[HttpPost]
		public IActionResult CreateTransaction([FromBody] TransactionDto transactionDto) {
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
				return Unauthorized();
			int userId = int.Parse(userIdClaim.Value);

			var isCategoryExist = db.Categories.Any(c => c.Id == transactionDto.CategoryId && (c.UserId == userId || c.UserId == null));
			if (!isCategoryExist) return NotFound("Category dose not exist");

			var newTransaction = new Transaction {
				Id = 0,
				Note = transactionDto.Note ?? "",
				Amount = transactionDto.Amount,
				CategoryId = transactionDto.CategoryId,
				UserId = userId,
				CreatedAt = DateTime.UtcNow
			};

			db.Transactions.Add(newTransaction);
			logger.LogInformation("New Transaction Has been created");
			db.SaveChanges();

			return Created();
		}


		[HttpPut("{id}")]
		public IActionResult UpdateTransaction(int id, [FromBody] TransactionDto transactionDto) {
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
				return Unauthorized();
			int userId = int.Parse(userIdClaim.Value);

			var transactionFromDB = db.Transactions.Find(id);
			if (transactionFromDB == null || transactionFromDB.UserId != userId) return NotFound("Transaction Not Found");

			var isCategoryExist = db.Categories.Any(c => c.Id == transactionDto.CategoryId && (c.UserId == userId || c.UserId == null));
			if (!isCategoryExist) return NotFound("Category dose not exist");

			transactionFromDB.Amount = transactionDto.Amount;
			transactionFromDB.Note = transactionDto.Note;
			transactionFromDB.CategoryId = transactionDto.CategoryId;

			logger.LogInformation("transaction {} has been updated", id);
			db.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteTransaction(int id) {
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
				return Unauthorized();
			int userId = int.Parse(userIdClaim.Value);

			var transactionFromDB = db.Transactions.Find(id);
			if (transactionFromDB == null || transactionFromDB.UserId != userId) return NotFound("Transaction Not Found");

			db.Transactions.Remove(transactionFromDB);
			logger.LogInformation("transaction {} has been deleted", id);
			db.SaveChanges();
			return NoContent();
		}
	}
}
