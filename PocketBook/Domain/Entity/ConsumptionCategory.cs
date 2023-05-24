using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class ConsumptionCategory
    {
        public ConsumptionCategory()
        {
            Consumptions = new HashSet<Consumption>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(20, 2)")]
        public int Limit { get; set; }


        public virtual ICollection<Consumption> Consumptions { get; set; } = new List<Consumption>();

        [ForeignKey(nameof(UserId))]
        public virtual User UserNavigation { get; set; } = null!;
    }
}
