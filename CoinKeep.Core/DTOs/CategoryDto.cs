using System;

using CoinKeep.Core.Statics;

namespace CoinKeep.Core.DTOs;

public class CategoryDto {
	public string Name { get; set; } = string.Empty;
	public CategoryType Type { get; set; } = CategoryType.Outcome;

}
