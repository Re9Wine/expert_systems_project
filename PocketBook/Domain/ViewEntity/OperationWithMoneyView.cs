using Domain.DatabaseEntity;

namespace Domain.ViewEntity
{
    public class OperationWithMoneyView
    {
        public Guid Id { get; set; }
        public bool IsConsumption { get; set; }
        public string CategoryName { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public decimal Value { get; set; }

        public OperationWithMoneyView() { }

        public OperationWithMoneyView(OperationWithMoney operationWithMoney)
        {
            Id = operationWithMoney.Id;
            IsConsumption = operationWithMoney.OperationCategoryNavigation.IsConsumption;
            CategoryName = operationWithMoney.OperationCategoryNavigation.Name;
            Date = operationWithMoney.Date;
            Description = operationWithMoney.Description;
            Value = operationWithMoney.Value;
        }
    }
}
