using System.Linq.Expressions;
using PocketBook.Domain.Entities;

namespace PocketBook.DAL.Repositories.BaseRepositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null, int? take = null);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
}