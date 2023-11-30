using PocketBook.Domain.DTOs;

namespace PocketBook.Domain.Pages;

public class MoneyTransactionPage
{
    public IEnumerable<MoneyTransactionDTO> Transactions { get; set; } = null!;
    public PageInfo PageInfo { get; set; } = null!;
}