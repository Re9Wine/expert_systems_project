using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Consumption
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ConsumptionGategoryId { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public DateOnly Date { get; set; }


        [ForeignKey(nameof(ConsumptionGategoryId))]
        public virtual ConsumptionCategory ConsumptionCategoryNavigation { get; set; } = null!;
    }
}
