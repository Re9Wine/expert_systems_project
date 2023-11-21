using System.ComponentModel.DataAnnotations;

namespace Domain.ViewEntity;

public class OperationCategoryView //TODO add error messages
{
    [Required(ErrorMessage = "")]
    [StringLength(100, ErrorMessage = "")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "")]
    [Range(0.0, 9999999999.99, ErrorMessage = "")]
    public decimal Limit { get; set; }
    
    [Required(ErrorMessage = "")]
    public bool IsConsumption { get; set; }
}