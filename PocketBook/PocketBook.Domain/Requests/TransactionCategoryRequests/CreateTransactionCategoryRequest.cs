using System.ComponentModel.DataAnnotations;
using PocketBook.Domain.Resources;

namespace PocketBook.Domain.Requests.TransactionCategoryRequests;

public class CreateTransactionCategoryRequest
{
    [Required(ErrorMessage = ValidationExceptionMessages.FieldIsRequired)]
    [StringLength(100, ErrorMessage = ValidationExceptionMessages.OutOfStringMaxLenghtFormat)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = ValidationExceptionMessages.FieldIsRequired)]
    [Range(0.0, 9999999999.99, ErrorMessage = ValidationExceptionMessages.OutOfDecimalPositiveValueFormal)]
    public decimal Limit { get; set; }
    
    // [Required(ErrorMessage = ValidationExceptionMessages.FieldIsRequired)]
    // public bool IsConsumption { get; set; }
}