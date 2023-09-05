using Domain.DatabaseEntity;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public virtual DbSet<OperationCategory> OperationCategories => Set<OperationCategory>();
        public virtual DbSet<OperationWithMoney> OperationWithMoneys => Set<OperationWithMoney>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PocketBook;Username=postgres;Password=123");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperationCategory>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__name_CC").IsUnique();
            });
        }
    }
}
