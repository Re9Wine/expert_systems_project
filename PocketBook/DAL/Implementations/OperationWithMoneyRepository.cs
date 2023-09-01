using DAL.Interfaces;
using Domain.DatabaseEntity;
using Domain.ViewEntity;
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

        public async Task<bool> Create(OperationWithMoney entity)
        {
            if (entity == null)
            {
                return false;
            }

            _context.OperationWithMoneys.Add(entity);

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> Delete(OperationWithMoney entity)
        {
            if (entity == null)
            {
                return false;
            }

            _context.OperationWithMoneys.Remove(entity);

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<OperationWithMoney?> GetById(Guid id)
        {
            return await _context.OperationWithMoneys.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OperationWithMoneyView>> GetFiveLatest(bool isConsumption)
        {
            return await _context.OperationWithMoneys
                .Include(x => x.OperationCategoryNavigation)
                .Where(x => x.OperationCategoryNavigation.IsConsumption == isConsumption)
                .OrderBy(x => x.Date)
                .Take(5)
                .Select(x => new OperationWithMoneyView(x))
                .ToListAsync();
        }

        public async Task<List<OperationWithMoneyView>> GetForPeriod(bool isConsumption, DateTime date)
        {
            return await _context.OperationWithMoneys
                .Include(x => x.OperationCategoryNavigation)
                .Where(x => x.OperationCategoryNavigation.IsConsumption == isConsumption && x.Date >= date)
                .Select(x => new OperationWithMoneyView(x))
                .ToListAsync();
        }
    }
}
