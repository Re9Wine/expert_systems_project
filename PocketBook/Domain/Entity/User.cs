using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Login { get; set; } = null!;

        [Required]
        [StringLength(100)]
        [PasswordPropertyText(true)]
        public string Password { get; set; } = null!;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public UserRole Role { get; set; }


        public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();
        public virtual ICollection<IncomeCategory> IncomeCategories { get; set; } = new List<IncomeCategory>();
        public virtual ICollection<Consumption> Consumptions { get; set; } = new List<Consumption>();
        public virtual ICollection<ConsumptionCategory> ConsumptionCategories { get; set; } = new List<ConsumptionCategory>();
    }
}
