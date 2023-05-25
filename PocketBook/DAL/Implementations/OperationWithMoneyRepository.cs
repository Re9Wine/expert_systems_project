using DAL.Interfaces;
using Domain.Entity;
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

        public Task<bool> Create(OperationWithMoney entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.OperationWithMoneys.Add(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<bool> Delete(OperationWithMoney entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.OperationWithMoneys.Remove(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<OperationWithMoney?> GetById(Guid id)
        {
            return _context.OperationWithMoneys.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
