namespace Domain.DatabaseEntity;

public class OperationWithMoney
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public string Description { get; set; } = null!;
        
    public decimal Value { get; set; }
        
    public DateTime Date { get; set; }

        
    public virtual OperationCategory OperationCategoryNavigation { get; set; } = null!;
}