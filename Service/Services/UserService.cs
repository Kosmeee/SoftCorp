using Microsoft.EntityFrameworkCore;
using Service.Cache;
using Service.Contexts;
using Service.EnumHelpers;
using Service.Models;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext context;
        private readonly ICustomCache cache;
        private List<Account> _accounts = new List<Account>
        {
            new Account { Id = 1, Username = "test", Password = "test" }
        };
        public UserService(UserDbContext context, ICustomCache cache)
        {
            this.context = context;
            this.cache = cache;
        }

        public async Task<Account> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _accounts.SingleOrDefault(x => x.Username == username && x.Password == password));
            return user;
        }

        public async Task<bool> Create(User user)
        {
            if (!cache.TryAddValue(user)) return false;
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return true;

        }

        public bool Remove(int id, out User user)
        {
            var removeUser = context.Users.FirstOrDefault(c => c.Id == id);
            if (removeUser != null)
            {
                context.Users.Remove(removeUser);
                
                context.SaveChanges();
            }
            var result = cache.TryRemoveValue(id, out user);

            return result;

        }

        public bool UserInfo(int id, out User user)
        {
            return cache.TryGetValue(id, out user);
            
        }

        public async Task<User> SetStatus(int id, StatusEnum status)
        {
            var user =await context.Users.FirstOrDefaultAsync(c => c.Id == id);
            user.Status = status;
            cache.TryUpdateValue(user);
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }

        public void FetchData(object? state)
        {
            var users = context.Users.AsNoTracking().ToList();
            cache.Clear();
            foreach (var user in users)
            {
                cache.TryAddValue(user);
            }
        }
    }
}
