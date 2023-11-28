using System.ComponentModel.DataAnnotations.Schema;

namespace PocketBook.Domain.Entities;

public class TransactionCategory : BaseEntity
{
    public string Name { get; set; } = null!;
    public short Priority { get; set; }
    public decimal Limit { get; set; }
    public bool IsChangeable { get; set; }
    public bool IsConsumption { get; set; }
    
    public virtual List<MoneyTransaction> MoneyTransactions { get; set; } = new();
}