using System.ComponentModel.DataAnnotations;

namespace Domain.ViewEntity;

public class OperationWithMoneyView //TODO add error messages
{
    [Required]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "")]
    [StringLength(100, ErrorMessage = "")]
    public required string Category { get; set; }

    [Required(ErrorMessage = "")]
    [StringLength(100, ErrorMessage = "")]
    public required string Description { get; set; }
    
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "")]
    [Range(0.0, 99999999.99, ErrorMessage = "")]
    public decimal Value { get; set; }
}