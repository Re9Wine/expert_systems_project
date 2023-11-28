using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PocketBook.DAL.DbContexts;
using PocketBook.Domain.Entities;

namespace PocketBook.DAL.Repositories.BaseRepositories;

internal class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly PocketBookDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(PocketBookDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null, int? take = null)
    {
        var query = _dbSet.AsQueryable();
        
        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }
        
        if (skip is not null && take is not null)
        {
            query = query.Skip(skip.Value).Take(take.Value);
        }

        return query.AsNoTracking().ToListAsync();
    }

    public Task<TEntity?> GetByIdAsync(Guid id)
    {
        return _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = _dbSet.Add(entity).Entity;

        await _context.SaveChangesAsync();

        return addedEntity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Modified)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        
        return true;
    }
}