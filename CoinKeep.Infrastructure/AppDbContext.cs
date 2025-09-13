using Microsoft.EntityFrameworkCore;
using CoinKeep.Core.Models;
using Microsoft.AspNetCore.Identity;
using CoinKeep.Core.Statics;

namespace CoinKeep.Infrastructure {
	public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {

		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			var hasher = new PasswordHasher<string>();

			modelBuilder.Entity<User>().HasData(
				new User {
					Id = 1,
					Username = "user1",
					Email = "user1@user.com",
					// Password = hasher.HashPassword("admin", "user1user1"),
					Password = "AQAAAAIAAYagAAAAEM4cZmJc1kpoIQqEjLhJYlMv6+yUuOfjR+LJoPl+au+YQ==", // precomputed
					CreatedAt = new DateTime(2025, 1, 1)
				}, new User {
					Id = 2,
					Username = "user2",
					Email = "user2@user.com",
					// Password = hasher.HashPassword("user2", "user2user2"),
					Password = "AQAAAAIAAYagAAAAEGD7ByIckeymf+kqAcPtLoqJ7Q9MmtLmDUwVJHhZsdWnY==",
					CreatedAt = new DateTime(2025, 1, 1)
				}, new User {
					Id = 3,
					Username = "user3",
					Email = "user3@user.com",
					// Password = hasher.HashPassword("user3", "user3user3"),
					Password = "AQAAAAIAAYagAAAAEO9zkXe19S1v+T17mLRQUGqFc0R0Q==",
					CreatedAt = new DateTime(2025, 1, 1)
				}
			);

			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, UserId = null, Name = "Salary", Type = CategoryType.Income },
				new Category { Id = 2, UserId = null, Name = "Freelance", Type = CategoryType.Income },
				new Category { Id = 3, UserId = null, Name = "Interest", Type = CategoryType.Income },
				new Category { Id = 4, UserId = null, Name = "Groceries", Type = CategoryType.Outcome },
				new Category { Id = 5, UserId = null, Name = "Rent", Type = CategoryType.Outcome },
				new Category { Id = 6, UserId = null, Name = "Utilities", Type = CategoryType.Outcome },
				new Category { Id = 7, UserId = null, Name = "Entertainment", Type = CategoryType.Outcome },
				new Category { Id = 8, UserId = null, Name = "Transport", Type = CategoryType.Outcome },
				new Category { Id = 9, UserId = null, Name = "Investment", Type = CategoryType.Income },
				new Category { Id = 10, UserId = null, Name = "Health", Type = CategoryType.Outcome }
			);
		}
	}
}
