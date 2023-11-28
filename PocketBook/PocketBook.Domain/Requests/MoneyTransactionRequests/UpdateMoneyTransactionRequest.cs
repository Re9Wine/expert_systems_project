using System.ComponentModel.DataAnnotations;
using PocketBook.Domain.Resources;

namespace PocketBook.Domain.Requests.MoneyTransactionRequests;

public class UpdateMoneyTransactionRequest
{
    [Required(ErrorMessage = ValidationExceptionMessages.UpdatingObjectIsNotSelected)]
    public Guid Id { get; set; }

    [Required(ErrorMessage = ValidationExceptionMessages.FieldIsRequired)]
    [StringLength(100, ErrorMessage = ValidationExceptionMessages.OutOfStringMaxLenghtFormat)]
    public string Category { get; set; } = null!;

    [Required(ErrorMessage = ValidationExceptionMessages.FieldIsRequired)]
    [StringLength(100, ErrorMessage = ValidationExceptionMessages.OutOfStringMaxLenghtFormat)]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = ValidationExceptionMessages.FieldIsRequired)]
    [Range(0.0, 99999999.99, ErrorMessage = ValidationExceptionMessages.OutOfDecimalPositiveValueFormal)]
    public decimal Value { get; set; }
}