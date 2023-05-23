using DAL.Interfaces;
using Domain.Entity;
using Service.Interfaces;

namespace Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AdminRegistration(User user)
        {
            if (user == null)
            {
                return false;
            }

            if (await _repository.ContainsEmail(user.Email) || await _repository.ContainsLogin(user.Login))
            {
                return false;
            }

            user.Role = UserRole.Admin;

            return await _repository.Create(user);
        }

        public async Task<bool> Authorization(string login, string password)
        {
            var user = await _repository.GetByLogin(login);

            if (user == null)
            {
                return false;
            }

            if (user.Password == password)
            {
                return true;
            }

            return false;
        }

        public Task<bool> ChangeUserData(User user)
        {
            if (user == null)
            {
                return Task.FromResult(false);
            }

            return _repository.Update(user);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _repository.GetById(id);

            if (user == null)
            {
                return false;
            }

            return await _repository.Delete(user);
        }

        public async Task<bool> Registration(User user)
        {
            if (user == null)
            {
                return false;
            }

            if (await _repository.ContainsEmail(user.Email) || await _repository.ContainsLogin(user.Login))
            {
                return false;
            }

            user.Role = UserRole.User;

            return await _repository.Create(user);
        }
    }
}
