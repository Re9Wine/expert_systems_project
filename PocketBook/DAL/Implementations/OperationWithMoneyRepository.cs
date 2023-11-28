using DAL.Interfaces;
using Domain.DatabaseEntity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class OperationWithMoneyRepository : IOperationWithMoneyRepository
{
    public async Task<List<OperationWithMoney>> GetAll()
        {
            return await _context.OperationWithMoneys.ToListAsync();
        }
    private readonly ApplicationDbContext _context;

    public OperationWithMoneyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationWithMoney?> GetByIdAsync(Guid id) // TODO добавить категорию 
    {
        return await _context.OperationWithMoneys.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> CreateAsync(OperationWithMoney entity)
    {
        _context.OperationWithMoneys.Add(entity);
            
        await _context.SaveChangesAsync();
            
        return true;
    }

    public async Task<bool> UpdateAsync(OperationWithMoney entity)
    {
        _context.OperationWithMoneys.Update(entity);
            
        await _context.SaveChangesAsync();
            
        return true;
    }

    public async Task<bool> DeleteAsync(OperationWithMoney entity)
    {
        _context.OperationWithMoneys.Remove(entity);
            
        await _context.SaveChangesAsync();
            
        return true;
    }

    public async Task<List<OperationWithMoney>> GetRangeWithCategoriesAsync(bool isConsumption, int count,
        int skip)
    {
        return await _context.OperationWithMoneys.Include(e => e.OperationCategoryNavigation)
            .Where(e => e.OperationCategoryNavigation.IsConsumption.Equals(isConsumption)).Skip(skip).Take(count).ToListAsync();
    }

    public async Task<List<OperationWithMoney>> GetPerPeriodWithCategoriesAsync(bool isConsumption,
        DateTime periodStart, DateTime periodEnd)
    {
        return await _context.OperationWithMoneys.Include(e => e.OperationCategoryNavigation).Where(e =>
                e.OperationCategoryNavigation.IsConsumption.Equals(isConsumption) &&
                e.Date >= periodStart &&
                e.Date <= periodEnd)
            .ToListAsync();
    }

    public decimal GetMonthlyIncome()
    {
        return _context.OperationWithMoneys.Include(e => e.OperationCategoryNavigation)
            .Where(e => !e.OperationCategoryNavigation.IsConsumption).Sum(e => e.Value);
    }
}