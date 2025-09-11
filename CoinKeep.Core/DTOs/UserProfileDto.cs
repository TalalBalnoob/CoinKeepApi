using System;

namespace CoinKeep.Core.DTOs;

public class UserProfileDto {
	public int Id { get; set; }
	public string Username { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	// Add only the fields you want exposed
}
