using Domain.Entity;

namespace DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User?> GetByLogin(string login);
        public Task<bool> ContainsLogin(string login);
        public Task<bool> ContainsEmail(string email);
    }
}
