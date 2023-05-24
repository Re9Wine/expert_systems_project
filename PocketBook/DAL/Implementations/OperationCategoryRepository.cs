using DAL.Interfaces;
using Domain.Entity;
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

        public Task<bool> Create(OperationCategory entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.OperationCategories.Add(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<bool> Delete(OperationCategory entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.OperationCategories.Remove(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }

        public Task<List<OperationCategory>> GetAll()
        {
            return _context.OperationCategories.ToListAsync();
        }

        public Task<OperationCategory?> GetById(Guid id)
        {
            return _context.OperationCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<OperationCategory>> GetByType(string type)
        {
            return _context.OperationCategories.Where(x => x.Type == type).ToListAsync();
        }

        public Task<bool> Update(OperationCategory entity)
        {
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _context.OperationCategories.Update(entity);

            return Task.FromResult(_context.SaveChangesAsync().Result != 0);
        }
    }
}
