using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ApplicationDbContext _context;

        public IncomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> Create(Income entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.Incomes.Add(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<bool> Delete(Income entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.Incomes.Remove(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<List<Income>> GetAll()
        {
            return _context.Incomes.ToListAsync();
        }

        public Task<Income?> GetById(Guid id)
        {
            return _context.Incomes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<bool> Update(Income entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.Incomes.Update(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }
    }
}
