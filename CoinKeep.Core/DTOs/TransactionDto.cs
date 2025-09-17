using System;

namespace CoinKeep.Core.DTOs;

public class TransactionDto {
	public decimal Amount { get; set; }
	public string? Note { get; set; }
	public int CategoryId { get; set; }
	public int accountId { get; set; }

}
