using Domain.Entity;

namespace Service.Interfaces
{
    public interface IUserService
    {
        public Task<bool> Authorization(string login, string password);
        public Task<bool> Registration(User user);
        public Task<bool> AdminRegistration(User user);
        public Task<bool> ChangeUserData(User user);
        public Task<bool> DeleteUser(Guid id);
    }
}
