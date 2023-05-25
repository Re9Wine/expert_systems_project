using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.View
{
    public class OperationWithMoneyForTableView
    {
        [Column(TypeName = "timestamp without time zone")]
        public DateTime Date { get; set; }

        public string Type { get; set; } = null!;

        [StringLength(100)]
        public string Category { get; set; } = null!;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }

        [StringLength(100)]
        public string Description { get; set; } = null!;
    }
}
