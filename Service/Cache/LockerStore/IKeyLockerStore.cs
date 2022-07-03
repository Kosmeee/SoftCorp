namespace Service.Cache.LockerStore
{
    public interface IKeyLockerStore
    {
        public object GetLockerFor(int key);
    }
}
