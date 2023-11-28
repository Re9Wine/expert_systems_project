using PocketBook.DAL.Repositories.BaseRepositories;
using PocketBook.Domain.Entities;

namespace PocketBook.DAL.Repositories.TransactionCategoryRepositories;

public interface ITransactionCategoryRepository : IBaseRepository<TransactionCategory>
{
    Task<TransactionCategory?> GetByNameAsync(string name);
    Task UpdateRangeAsync(List<TransactionCategory> categories);
}