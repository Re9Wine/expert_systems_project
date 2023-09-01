using DAL.Interfaces;
using Domain.DatabaseEntity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class OperationCategoryRepository : IOperationCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public OperationCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(OperationCategory entity)
        {
            if (entity == null)
            {
                return false;
            }

            _context.OperationCategories.Add(entity);

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<OperationCategory?> GetById(Guid id)
        {
            return await _context.OperationCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OperationCategory>> GetByType(bool isConsumption)
        {
            return await _context.OperationCategories.Where(x => x.IsConsumption == isConsumption).ToListAsync();
        }

        public async Task<bool> Update(OperationCategory entity)
        {
            if (entity == null)
            {
                return false;
            }

            _context.OperationCategories.Update(entity);

            return await _context.SaveChangesAsync() != 0;
        }
    }
}
