using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class OperationCategory
    {
        public OperationCategory()
        {
            OperationWithMoneys = new HashSet<OperationWithMoney>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Type { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(20, 2)")]
        public int Limit { get; set; }

        public virtual ICollection<OperationWithMoney> OperationWithMoneys { get; set; }
    }
}
