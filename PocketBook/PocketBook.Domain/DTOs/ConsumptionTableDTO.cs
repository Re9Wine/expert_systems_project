namespace PocketBook.Domain.DTOs;

public class ConsumptionTableDTO
{
    public string Category { get; set; } = null!;
    public decimal Sum { get; set; }
    public decimal Limit { get; set; }
}