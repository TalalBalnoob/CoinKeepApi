using Microsoft.EntityFrameworkCore;
using CoinKeep.Core.Models;

namespace CoinKeep.Infrastructure {
	public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {

		public DbSet<User> Users { get; set; }
	}
}
