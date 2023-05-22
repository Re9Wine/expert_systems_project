using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class IncomeCategory
    {
        public IncomeCategory()
        {
            Incomes = new HashSet<Income>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(20, 2)")]
        public decimal Limit { get; set; }


        public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

        [ForeignKey(nameof(UserId))]
        public virtual User UserNavigation { get; set; } = null!;
    }
}
