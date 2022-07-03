using System.Collections.Concurrent;
using Service.Cache.LockerStore;
using Service.Models;

namespace Service.Cache
{
    public class CustomCache : ICustomCache
    {
        private readonly IKeyLockerStore _keyLockerStore;
        readonly ConcurrentDictionary<int, User> _cache = new();

        public CustomCache( IKeyLockerStore keyLockerStore)
        {
            _keyLockerStore = keyLockerStore;
        }

        public bool TryAddValue(User user)
        {
            lock (_keyLockerStore.GetLockerFor(user.Id))
            {
                return _cache.TryAdd(user.Id, user);
            }
        }

        public bool TryGetValue(int key, out User user)
        {
            lock (_keyLockerStore.GetLockerFor(key))
            {
                return _cache.TryGetValue(key, out user);
            }
        }

        public bool TryUpdateValue(User user)
        {
            lock (_keyLockerStore.GetLockerFor(user.Id))
            {
                if (!_cache.ContainsKey(user.Id)) return false;
                _cache[user.Id] = user;
                return true;

            }
        }

        public bool TryRemoveValue(int key, out User user)
        {
            lock (_keyLockerStore.GetLockerFor(key))
            {
                return _cache.TryRemove(key, out user);
            }
        }

        public void Clear()
        {
            _cache.Clear();
        }

        

    }
}
