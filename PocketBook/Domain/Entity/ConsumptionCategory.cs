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

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Image { get; set; } = null!;


        public virtual ICollection<Consumption>? Consumptions { get; set; }
    }
}
