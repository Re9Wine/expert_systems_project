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
        public required string Name { get; set; }

        [Required]
        public int Priority { get; set; }

        [DefaultValue(true)]
        public bool IsChangeable { get; set; } = true;


        public virtual ICollection<OperationWithMoney> OperationWithMoneys { get; set; }
    }
}
