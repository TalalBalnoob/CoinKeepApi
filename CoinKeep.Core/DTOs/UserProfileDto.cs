using System;

using CoinKeep.Core.Models;

namespace CoinKeep.Core.DTOs;

public class UserProfileDto {
	public int Id { get; set; }
	public string Username { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public ReturnedAccountDTO? Account { get; set; }
	// Add only the fields you want exposed
}
