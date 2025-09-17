using Microsoft.EntityFrameworkCore;
using CoinKeep.Core.Models;
using Microsoft.AspNetCore.Identity;
using CoinKeep.Core.Statics;

namespace CoinKeep.Infrastructure {
	public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {

		public DbSet<User> Users { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Transaction>()
			   .HasOne(t => t.Category)
			   .WithMany(c => c.Transactions)
			   .HasForeignKey(t => t.CategoryId)
			   .OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<User>().HasData(
				new User {
					Id = 1,
					Username = "user1",
					Email = "user1@user.com",
					// Password = hasher.HashPassword("user1", "user1user1"),
					Password = "AQAAAAIAAYagAAAAEBWS00dIwzm6aum6zfyyxPsavjBxHL4bH+WALNgFmTtJTu6hlIINddyY2+bJN2n6pg==", // precomputed
					CreatedAt = new DateTime(2025, 1, 1)
				}, new User {
					Id = 2,
					Username = "user2",
					Email = "user2@user.com",
					// Password = hasher.HashPassword("user2", "user2user2"),
					Password = "AQAAAAIAAYagAAAAED/hSj4vRPNwMlSSpSqSdf5JsMYUe4hNXVgSjIsq8KHfvnB8lUUYs9Ujei9bKSdIkQ==",
					CreatedAt = new DateTime(2025, 1, 1)
				}, new User {
					Id = 3,
					Username = "user3",
					Email = "user3@user.com",
					// Password = hasher.HashPassword("user3", "user3user3"),
					Password = "AQAAAAIAAYagAAAAEE2Vx1XhcxppJObpFP2V+sm3WzE8LiX3BhF78UyKFwiK20xTn2n7jx2U9T8hnZFr+Q==",
					CreatedAt = new DateTime(2025, 1, 1)
				}
			);

			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 0, UserId = null, Name = "Balance Adjustment", Type = CategoryType.Adjustment },
				new Category { Id = 1, UserId = null, Name = "Salary", Type = CategoryType.Income },
				new Category { Id = 2, UserId = null, Name = "Freelance", Type = CategoryType.Income },
				new Category { Id = 3, UserId = null, Name = "Interest", Type = CategoryType.Income },
				new Category { Id = 4, UserId = null, Name = "Groceries", Type = CategoryType.Expense },
				new Category { Id = 5, UserId = null, Name = "Rent", Type = CategoryType.Expense },
				new Category { Id = 6, UserId = null, Name = "Utilities", Type = CategoryType.Expense },
				new Category { Id = 7, UserId = null, Name = "Entertainment", Type = CategoryType.Expense },
				new Category { Id = 8, UserId = null, Name = "Transport", Type = CategoryType.Expense },
				new Category { Id = 9, UserId = null, Name = "Investment", Type = CategoryType.Income },
				new Category { Id = 10, UserId = null, Name = "Health", Type = CategoryType.Expense }
			);
		}
	}
}
