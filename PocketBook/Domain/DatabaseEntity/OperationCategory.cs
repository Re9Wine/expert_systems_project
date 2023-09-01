using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DatabaseEntity
{
    [Table("OperationCategory")]
    public class OperationCategory
    {
        public OperationCategory()
        {
            OperationWithMoneys = new HashSet<OperationWithMoney>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsConsumption { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsChangeable { get; set; } = false;


        public virtual ICollection<OperationWithMoney> OperationWithMoneys { get; set; }
    }
}
