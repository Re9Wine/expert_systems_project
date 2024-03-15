namespace PocketBook.Domain.DTOs;

public class ForecastByCategoryDTO
{
    public required string Category { get; set; }
    public required decimal Forecast { get; set; }
}