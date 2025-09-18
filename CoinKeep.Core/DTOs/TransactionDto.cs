using System;

namespace CoinKeep.Core.DTOs;

public class NewTransactionDto {
	public decimal Amount { get; set; }
	public string? Note { get; set; }
	public int CategoryId { get; set; }

}
