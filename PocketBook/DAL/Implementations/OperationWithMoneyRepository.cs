using DAL.Interfaces;
using Domain.DatabaseEntity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class OperationWithMoneyRepository : IOperationWithMoneyRepository
    {
        private readonly ApplicationDbContext _context;

        public OperationWithMoneyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(OperationWithMoney entity)
        {
            if (entity == null)
            {
                return false;
            }

            _context.OperationWithMoneys.Add(entity);

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> DeleteAsync(OperationWithMoney entity)
        {
            if (entity == null)
            {
                return false;
            }

            _context.OperationWithMoneys.Remove(entity);

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<OperationWithMoney?> GetByIdAsync(Guid id)
        {
            return await _context.OperationWithMoneys.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OperationWithMoney>> GetPerPeriodAsync
            (bool isConusption, DateTime periodBeginnig, DateTime periodEnd)
        {
            return await _context.OperationWithMoneys
                .Where(x => x.IsConsumption == isConusption &&
                            x.Date >= periodBeginnig &&
                            x.Date <= periodEnd)
                .ToListAsync();
        }

        public async Task<List<OperationWithMoney>> GetRangeWihtCategoriesAsync(bool isConsumption, int amount, int skip)
        {
            return await _context.OperationWithMoneys
                .Include(x => x.OperationCategoryNavigation)
                .Where(x => x.IsConsumption == isConsumption)
                .Skip(skip)
                .Take(amount)
                .ToListAsync();
        }
    }
}
