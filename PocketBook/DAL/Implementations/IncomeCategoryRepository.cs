using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class IncomeCategoryRepository : IIncomeCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public IncomeCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> Create(IncomeCategory entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.IncomeCategories.Add(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<bool> Delete(IncomeCategory entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.IncomeCategories.Remove(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<List<IncomeCategory>> GetAll()
        {
            return _context.IncomeCategories.ToListAsync();
        }

        public Task<IncomeCategory?> GetById(Guid id)
        {
            return _context.IncomeCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<bool> Update(IncomeCategory entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.IncomeCategories.Update(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }
    }
}
