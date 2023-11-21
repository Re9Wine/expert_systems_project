using DAL.Interfaces;
using Domain.DatabaseEntity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class OperationCategoryRepository : IOperationCategoryRepository
{
    private readonly ApplicationDbContext _context;

    public OperationCategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationCategory?> GetByIdAsync(Guid id)
    {
        return await _context.OperationCategories.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> CreateAsync(OperationCategory entity)
    {
        _context.OperationCategories.Add(entity);
    
        await _context.SaveChangesAsync();
    
        return true;
    }

    public async Task<bool> UpdateAsync(OperationCategory entity)
    {
        _context.OperationCategories.Update(entity);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(OperationCategory entity)
    {
        _context.OperationCategories.Remove(entity);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<OperationCategory?> GetByNameAsync(string name)
    {
        return await _context.OperationCategories.FirstOrDefaultAsync(e => e.Name.Equals(name));
    }

    public async Task<List<OperationCategory>> GetRangeAsync(int count, int skip)
    {
        return await _context.OperationCategories.Skip(skip).Take(count).ToListAsync();
    }

    public async Task<List<OperationCategory>> GetAllConsumption()
    {
        return await _context.OperationCategories.Where(e => e.IsConsumption).ToListAsync();
    }

    public async Task<bool> UpdateRangeAsync(List<OperationCategory> categories)
    {
        _context.OperationCategories.UpdateRange(categories);

        await _context.SaveChangesAsync();

        return true;
    }
}