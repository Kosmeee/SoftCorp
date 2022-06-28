using System.Collections.Concurrent;

namespace Service.Cache.LockerStore
{
    public class DictionaryKeyLockerStore : IKeyLockerStore
    {
        private readonly ConcurrentDictionary<int, object> _keyLockerMap;

        public DictionaryKeyLockerStore(ConcurrentDictionary<int, object> keyLockerMap)
        {
            _keyLockerMap = keyLockerMap;
        }

        public object GetLockerFor(int key)
        {
            return _keyLockerMap.GetOrAdd(key, k => new object());
        }
    }
}
