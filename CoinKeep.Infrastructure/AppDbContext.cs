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
					Password = hasher.HashPassword("admin", "user1user1"),
				}, new User {
					Id = 2,
					Username = "user2",
					Password = hasher.HashPassword("user2", "user2user2"),
				}, new User {
					Id = 3,
					Username = "user3",
					Password = hasher.HashPassword("user3", "user3user3"),
				}
			);
		}
	}
}
