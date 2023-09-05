namespace Domain.ViewEntity
{
    public class OperationCategoryView
    {
        public DateTime Date { get; set; }
        public string Category { get; set; } = null!;
        public decimal Sum { get; set; }
    }
}
