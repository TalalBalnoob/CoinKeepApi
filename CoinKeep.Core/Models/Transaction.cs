using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace CoinKeep.Core.Models;

public class Transaction {

	[Key]
	public int Id { get; set; }

	[Required]
	public decimal Amount { get; set; }

	public string? Note { get; set; }

	public int? CategoryId { get; set; }


	[ForeignKey("CategoryId")]
	public Category? Category { get; set; }

	public int AccountId { get; set; }


	[ForeignKey("AccountId")]
	public Account? Account { get; set; }

	public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

}
