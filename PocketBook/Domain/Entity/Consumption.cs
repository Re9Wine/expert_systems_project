using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Consumption
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ConsumptionGategoryId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }

        [Required]
        public DateOnly Date { get; set; }


        [ForeignKey(nameof(ConsumptionGategoryId))]
        public virtual ConsumptionCategory ConsumptionCategoryNavigation { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public virtual User UserNavigation { get; set; } = null!;
    }
}
