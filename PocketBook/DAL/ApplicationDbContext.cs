using Domain.Entity;
using Domain.View;
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
        public virtual DbSet<OperationWithMoneyForTableView> OperationWithMoneyForTableViews => Set<OperationWithMoneyForTableView>();

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

            modelBuilder.Entity<OperationWithMoneyForTableView>(entity =>
            {
                modelBuilder.Entity<OperationWithMoneyForTableView>(entity =>
                {
                    entity
                        .HasNoKey()
                        .ToView("OperationWithMoneyForTableView");

                    entity.Property(e => e.Category).HasMaxLength(100);
                    entity.Property(e => e.Date).HasColumnType("timestamp without time zone");
                    entity.Property(e => e.Description).HasMaxLength(100);
                    entity.Property(e => e.Value).HasPrecision(10, 2);
                });
            });
        }
    }
}
