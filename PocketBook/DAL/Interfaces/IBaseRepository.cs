namespace DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T?> GetById(Guid id);
        Task<List<T>> GetAll();
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
