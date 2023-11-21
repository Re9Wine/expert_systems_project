namespace Domain.DatabaseEntity;

public class OperationCategory
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
        
    public int Priority { get; set; }
        
    public decimal Limit { get; set; }
        
    public bool IsChangeable { get; set; }
    
    public bool IsConsumption { get; set; }


    public virtual ICollection<OperationWithMoney> OperationWithMoneys { get; set; } =
        new List<OperationWithMoney>();
}