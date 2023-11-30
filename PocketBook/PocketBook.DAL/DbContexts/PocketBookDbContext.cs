using Microsoft.EntityFrameworkCore;
using PocketBook.Domain.Entities;

namespace PocketBook.DAL.DbContexts;

public class PocketBookDbContext : DbContext
{
    public DbSet<TransactionCategory> TransactionCategories => Set<TransactionCategory>();
    public DbSet<MoneyTransaction> MoneyTransactions => Set<MoneyTransaction>();
    
    public PocketBookDbContext(DbContextOptions<PocketBookDbContext> options) : base(options)
    {
    }
}