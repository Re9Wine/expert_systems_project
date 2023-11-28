using Microsoft.EntityFrameworkCore;
using PocketBook.DAL.DbContexts;
using PocketBook.DAL.Repositories.BaseRepositories;
using PocketBook.Domain.Entities;

namespace PocketBook.DAL.Repositories.TransactionCategoryRepositories;

internal class TransactionCategoryRepository : BaseRepository<TransactionCategory>, ITransactionCategoryRepository
{
    public TransactionCategoryRepository(PocketBookDbContext context) : base(context)
    {
    }

    public Task<TransactionCategory?> GetByNameAsync(string name)
    {
        return _dbSet.FirstOrDefaultAsync(category => category.Name == name);
    }

    public async Task UpdateRangeAsync(List<TransactionCategory> categories)
    {
        _dbSet.AttachRange(categories);

        foreach (var category in categories)
        {
            _context.Entry(category).State = EntityState.Modified;
        }
        
        await _context.SaveChangesAsync();
    }
}