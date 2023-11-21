namespace Domain.ViewEntity;

public class BarChartView
{
    public required DateTime Date { get; set; }
    public required decimal Sum { get; set; }
    public required List<string> ErrorMessages { get; set; }
}