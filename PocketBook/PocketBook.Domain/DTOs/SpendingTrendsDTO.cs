namespace PocketBook.Domain.DTOs;

public class SpendingTrendsDTO
{
    public required List<string> Categories { get; set; }
    public required MonthlyExpenses PreviousMonth { get; set; }
    public required MonthlyExpenses PreviousPreviousMonth { get; set; }
}

public class MonthlyExpenses
{
    public required string Name { get; set; } = null!;
    public required List<decimal> Values { get; set; } = new();
} 