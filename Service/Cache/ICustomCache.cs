using Service.Models;

namespace Service.Cache
{
    public interface ICustomCache
    {
        public bool TryAddValue(User user);
        public bool TryGetValue(int key, out User user);
        public bool TryUpdateValue(User user);
        public bool TryRemoveValue(int key, out User user);
        public void Clear();
    }
}
