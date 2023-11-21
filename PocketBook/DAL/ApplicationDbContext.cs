using Domain;
using Domain.DatabaseEntity;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public ApplicationDbContext() { }

    public DbSet<OperationCategory> OperationCategories => Set<OperationCategory>();
    public DbSet<OperationWithMoney> OperationWithMoneys => Set<OperationWithMoney>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OperationCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OperationCategory_pkey");
        
            // entity.ToTable("OperationCategory", t =>
            // {
            //     t.HasCheckConstraint("Priority", "Priority > -1 AND Priority < 11");
            //     t.HasCheckConstraint("Limit", "Limit > 0");
            // });
        
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Priority).IsRequired();
            entity.Property(e => e.Limit).HasColumnType("numeric(12, 2)").IsRequired();
            entity.Property(e => e.IsChangeable).IsRequired();
            entity.Property(e => e.IsConsumption).IsRequired();
        });
        // TODO протестить
        modelBuilder.Entity<OperationCategory>().HasData(Resources.StandardCategories.Select(_ => _.Value));
        
        modelBuilder.Entity<OperationWithMoney>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OperationWithMoney_pkey");
            
            // entity.ToTable("OperationWithMoney", t =>
            // {
            //     t.HasCheckConstraint("Value", "Value > 0");
            // });
        
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Value).HasColumnType("numeric(10, 2)").IsRequired();
            entity.Property(e => e.Date).HasColumnType("timestamp without time zone").IsRequired();
        
            entity.HasOne(d => 
                    d.OperationCategoryNavigation).WithMany(p => p.OperationWithMoneys)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OperationWithMoney_CategoryId_fkey");
        });
    }
}