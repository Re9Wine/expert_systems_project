namespace PocketBook.Domain.DTOs;

public class MoneyTransactionDTO
{
    public Guid Id { get; set; }
    public string Category { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
}