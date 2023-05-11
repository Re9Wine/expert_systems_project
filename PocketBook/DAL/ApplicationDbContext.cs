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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=usersdb;Username=postgres;Password=здесь_указывается_пароль_от_postgres"); // TODO разобраться в подключении
            }
        }
    }
}
