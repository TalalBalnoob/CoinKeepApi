using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinKeep.Core.Models;

public class Account {
	[Key]
	public int Id { get; set; }
	public int UserId { get; set; }
	public string Name { get; set; } = "";
	public decimal Balance { get; set; }

	[ForeignKey("UserId")]
	public User? User { get; set; }
	public ICollection<Transaction> Transactions { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


}
