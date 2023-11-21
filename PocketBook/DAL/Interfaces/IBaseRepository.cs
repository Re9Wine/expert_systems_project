namespace DAL.Interfaces;

public interface IBaseRepository<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}