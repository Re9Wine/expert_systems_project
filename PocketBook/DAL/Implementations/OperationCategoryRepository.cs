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

        public async Task<bool> CreateAsync(OperationCategory entity)
        {
            if(entity == null)
            {
                return false;
            }

            _context.OperationCategories.Add(entity);

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<List<OperationCategory>> GetAllAsync()
        {
            return await _context.OperationCategories.ToListAsync();
        }

        public async Task<OperationCategory?> GetByIdAsync(Guid id)
        {
            return await _context.OperationCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OperationCategory>> GetPerPeriodWithOperationsAsync
            (bool isConusption, DateTime periodBeginnig, DateTime periodEnd)
        {
            return await _context.OperationCategories
                .Include(x => x.OperationWithMoneys)
                .Where(x => x.OperationWithMoneys
                    .Where(y => y.IsConsumption == isConusption &&
                                y.Date >= periodBeginnig &&
                                y.Date <= periodEnd).Count() != 0)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(OperationCategory entity)
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
