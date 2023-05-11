using DAL.Interfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class ConsumptionRepository : IConsumptionRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsumptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> Create(Consumption entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.Consumptions.Add(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<bool> Delete(Consumption entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.Consumptions.Remove(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<List<Consumption>> GetAll()
        {
            return _context.Consumptions.ToListAsync();
        }

        public Task<Consumption?> GetById(Guid id)
        {
            return _context.Consumptions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<bool> Update(Consumption entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.Consumptions.Update(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }
    }
}
