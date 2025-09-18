using System;

namespace CoinKeep.Core.DTOs;

public class ReturnedTransactionDTO {
	public int Id { get; set; }
	public decimal Amount { get; set; }
	public string? Note { get; set; }
	public int? CategoryId { get; set; }
	public DateTime? CreatedAt { get; set; }
}
