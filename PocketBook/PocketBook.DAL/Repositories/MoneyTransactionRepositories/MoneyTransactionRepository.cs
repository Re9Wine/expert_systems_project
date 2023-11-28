using PocketBook.DAL.DbContexts;
using PocketBook.DAL.Repositories.BaseRepositories;
using PocketBook.Domain.Entities;

namespace PocketBook.DAL.Repositories.MoneyTransactionRepositories;

internal class MoneyTransactionRepository : BaseRepository<MoneyTransaction>, IMoneyTransactionRepository
{
    public MoneyTransactionRepository(PocketBookDbContext context) : base(context)
    {
    }
}