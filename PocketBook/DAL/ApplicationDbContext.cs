using Domain.Entity;
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
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Income> Incomes => Set<Income>();
        public DbSet<Consumption> Consumptions => Set<Consumption>();
        public DbSet<ConsumptionCategory> ConsumptionCategories => Set<ConsumptionCategory>();
        public DbSet<IncomeCategory> IncomeCategories => Set<IncomeCategory>();
        public DbSet<User> Users => Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PocketBook;Username=postgres;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Login, "UQ__login__U").IsUnique();
                entity.HasIndex(e => e.Email, "UQ__email__U").IsUnique();
            });

            modelBuilder.Entity<ConsumptionCategory>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__name_CC").IsUnique();
            });

            modelBuilder.Entity<IncomeCategory>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__name__IC").IsUnique();
            });
        }
    }
}
