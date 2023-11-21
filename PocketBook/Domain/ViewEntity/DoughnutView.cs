namespace Domain.ViewEntity;

public class DoughnutView
{
    public required string Category { get; set; }
    public required decimal Value { get; set; }
    public required List<string> ErrorMessages { get; set; }
}