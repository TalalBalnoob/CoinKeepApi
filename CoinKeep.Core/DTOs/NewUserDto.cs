using System;

namespace CoinKeep.Core.DTOs;

public class NewUserDto {
	public string Username { get; set; }
	public decimal initialBalance { get; set; }

	public string Email { get; set; }

	public string Password { get; set; }

}
