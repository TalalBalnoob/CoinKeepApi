using System.Security.Claims;

using CoinKeep.Api.Extensions;
using CoinKeep.Core.DTOs;
using CoinKeep.Core.Models;
using CoinKeep.Core.Statics;
using CoinKeep.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoinKeep.Api.Controllers {
	[Authorize]
	[Route("[controller]")]
	[ApiController]
	public class CategoryController(AppDbContext db) : ControllerBase {

		[HttpGet]
		public IActionResult getAllCategories() {
			var categories = db.Categories.Where(c => c.Type == CategoryType.Expense || c.Type == CategoryType.Income).ToList();

			return this.Ok(categories);
		}

		[HttpGet("{id}/transactions")]
		public IActionResult getRelatedTransactions(int id) {
			var userId = User.GetUserId();

			var category = db.Categories.Include(c => c.Transactions).FirstOrDefault(c => id == c.Id);
			if (category == null || category.UserId != userId) return NotFound();

			var transactions = category.Transactions.ToList();

			return Ok(transactions);
		}

		[HttpPost]
		public IActionResult AddCategory(CategoryDto categoryDto) {
			var userId = User.GetUserId();

			var isExist = db.Categories.Any(c => c.Name.ToLower() == categoryDto.Name.ToLower());
			if (isExist) return Conflict();

			var newCategory = new Category {
				Id = 0,
				Name = categoryDto.Name,
				Type = categoryDto.Type,
				UserId = userId,
			};

			db.Categories.Add(newCategory);
			db.SaveChanges();

			return Created();
		}

		[HttpPut("{id}")]
		public IActionResult UpdateCategory(int id, [FromBody] CategoryDto categoryDto) {
			var userId = User.GetUserId();

			var categoryFromDb = db.Categories.Find(id);
			if (categoryFromDb == null) return NotFound();

			var isNameExist = db.Categories.Any(c => c.Name.ToLower() == categoryDto.Name.ToLower() && c.Id != categoryFromDb.Id);
			if (isNameExist) return Conflict();

			categoryFromDb.Name = categoryDto.Name;
			categoryFromDb.Type = categoryFromDb.Type;

			db.SaveChanges();
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCategory(int id) {
			var userId = User.GetUserId();

			var categoryFromDb = db.Categories.First(c => c.Id == id && c.UserId == userId);
			if (categoryFromDb == null) return NotFound();

			db.Categories.Remove(categoryFromDb);
			db.SaveChanges();
			return NoContent();
		}
	}
}
