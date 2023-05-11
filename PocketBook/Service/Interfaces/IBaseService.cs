namespace Service.Interfaces
{
    public interface IBaseService<T>
    {
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> DeleteById(Guid id);
        Task<T?> GetById(Guid id);
        Task<List<T>> GetAll();
    }
}
