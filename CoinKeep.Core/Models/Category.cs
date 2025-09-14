using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CoinKeep.Core.Statics;

namespace CoinKeep.Core.Models;

public class Category {

	[Key]
	public int Id { get; set; }

	[Required]
	public string Name { get; set; } = string.Empty;
	public CategoryType Type { get; set; }
	public int? UserId { get; set; }


	[ForeignKey("UserId")]
	public User? User { get; set; }

	public ICollection<Transaction> Transactions { get; set; }

}
