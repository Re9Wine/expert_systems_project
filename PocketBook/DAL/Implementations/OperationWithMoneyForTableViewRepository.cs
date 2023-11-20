using DAL.Interfaces;
using Domain;
using Domain.View;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class OperationWithMoneyForTableViewRepository : IOperationWithMoneyForTableViewRepository
    {
        private readonly ApplicationDbContext _context;

        public OperationWithMoneyForTableViewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<OperationWithMoneyForTableView>> GetFiveLatestConsumption()
        {
            return _context.OperationWithMoneyForTableViews.Where(x => x.Type == Constant.Consumption).OrderByDescending(x => x.Date).Take(5).ToListAsync();
        }

        public Task<List<OperationWithMoneyForTableView>> GetConsumptionForPeriod(DateTime date)
        {
            return _context.OperationWithMoneyForTableViews.Where(x => x.Date >= date && x.Type == Constant.Consumption).ToListAsync();
        }
    }
}
