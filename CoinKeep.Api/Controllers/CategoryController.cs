using CoinKeep.Core.Models;
using CoinKeep.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoinKeep.Api.Controllers {
	[Authorize]
	[Route("[controller]")]
	[ApiController]
	public class CategoryController(AppDbContext db) : ControllerBase {

		[HttpGet]
		public IActionResult getAllCategories() {
			var categories = db.Categories.ToList();

			return this.Ok(categories);
		}
	}
}
