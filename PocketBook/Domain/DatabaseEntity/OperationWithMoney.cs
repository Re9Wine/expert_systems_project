using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DatabaseEntity
{
    [Table("OperationWithMoney")]
    public class OperationWithMoney
    {
        public OperationWithMoney()
        {
            var currentDate = DateTime.Now;

            Date = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
                currentDate.Hour, currentDate.Minute, currentDate.Second);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OperationId { get; set; }

        [Required]
        public bool IsConsumption { get; set; }

        [Required]
        [StringLength(100)]
        public required string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime Date { get; set; } = DateTime.Now;


        [ForeignKey(nameof(OperationId))]
        public virtual OperationCategory OperationCategoryNavigation { get; set; } = null!;
    }
}
