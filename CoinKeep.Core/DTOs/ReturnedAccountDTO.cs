using System;

namespace CoinKeep.Core.DTOs;

public class ReturnedAccountDTO {
	public int Id { get; set; }
	public string Name { get; set; } = "";
	public decimal Balance { get; set; }
	public List<ReturnedTransactionDTO> Transactions { get; set; }
}
