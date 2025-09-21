namespace CoinKeep.Core.Statics;

public enum CategoryType {
	Income,
	Expense,
	Adjustment
}

public static class CategoryTypeExtensions {
	public static string ToFriendlyString(this CategoryType categoryType) {
		return categoryType switch {
			CategoryType.Income => "Income",
			CategoryType.Expense => "Expense",
			CategoryType.Adjustment => "Adjustment",
			_ => "Unknown"
		};
	}
}
