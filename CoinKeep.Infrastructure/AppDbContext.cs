using Microsoft.EntityFrameworkCore;
using CoinKeep.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace CoinKeep.Infrastructure {
	public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {

		public DbSet<User> Users { get; set; }

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
		}
	}
}
