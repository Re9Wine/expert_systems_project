using System.ComponentModel.DataAnnotations.Schema;

namespace PocketBook.Domain.Entities;

[Table(nameof(MoneyTransaction))]
public class MoneyTransaction : BaseEntity
{
    public Guid TransactionCategoryId { get; set; }
    public string Description { get; set; } = null!;
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
    
    public virtual TransactionCategory TransactionCategory  { get; set; } = null!;
}