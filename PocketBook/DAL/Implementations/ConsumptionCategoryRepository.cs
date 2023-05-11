using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class ConsumptionCategoryRepository : IConsumptionCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsumptionCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> Create(ConsumptionCategory entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.ConsumptionCategories.Add(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<bool> Delete(ConsumptionCategory entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.ConsumptionCategories.Remove(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<List<ConsumptionCategory>> GetAll()
        {
            return _context.ConsumptionCategories.ToListAsync();
        }

        public Task<ConsumptionCategory?> GetById(Guid id)
        {
            return _context.ConsumptionCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<bool> Update(ConsumptionCategory entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.ConsumptionCategories.Update(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }
    }
}
