using Service.Models;

namespace Service.Services
{
    public interface IUserService
    {
        public Task<bool> Create(User user);
        public bool Remove(int id, out User user);
        public bool UserInfo(int id, out User user);
        public Task<User> SetStatus(int id, StatusEnum status);

        public void FetchData(object? state);

        public Task<Account> Authenticate(string username, string password);

    }
}
