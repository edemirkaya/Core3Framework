using Microsoft.Extensions.Caching.Memory;

namespace CommonCore.Caching
{
    public sealed class CustomMemoryCache : MemoryCache
    {
        IMemoryCache memoryCache;
        public CustomMemoryCache(IMemoryCache _memoryCache)
            : base(new MemoryCacheOptions())
        {
            memoryCache = _memoryCache;

        }

        public T Get<T>(string key) where T : class
        {
            object returnObject;
            base.TryGetValue(key, out returnObject);
            return (T)returnObject;
        }

        public void Set(string key, object value)
        {
            base.CreateEntry(key).Value = value;
        }
    }
}