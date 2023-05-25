using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class OperationWithMoney
    {
        public OperationWithMoney()
        {
            var bufferDate = DateTime.Now;

            Date = new DateTime(bufferDate.Year, bufferDate.Month, bufferDate.Day, bufferDate.Hour, bufferDate.Minute, bufferDate.Second);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OperationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime Date { get; set; }


        [ForeignKey(nameof(OperationId))]
        public virtual OperationCategory OperationCategoryNavigation { get; set; } = null!;
    }
}
