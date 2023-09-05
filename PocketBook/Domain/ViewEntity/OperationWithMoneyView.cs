namespace Domain.ViewEntity
{
    public class OperationWithMoneyView
    {
        public DateTime Date { get; set; }
        public required string CategoryName { get; set; }
        public decimal Value { get; set; }
        public required string Description { get; set; }
    }
}
