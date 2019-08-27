using Microsoft.Extensions.Caching.Memory;
using System;
using System.Diagnostics;
using System.Threading;

namespace CommonCore.Caching
{
    public abstract class CacheManagerBase
    {
        public IMemoryCache Cache { get; set; }


        public CacheManagerBase(IMemoryCache _memoryCache)
        {
            Cache = _memoryCache;

        }

        protected Lazy<TItem> GetLazyCacheItem<TItem>(string key, Func<TItem> itemCreateFunc, string policyName = "") where TItem : class
        {
            var item = default(Lazy<TItem>);

            object cacheItem;
            Cache.TryGetValue(key, out cacheItem);
            if (cacheItem != null)
            {
                item = Cache.Get<Lazy<TItem>>(key);

                //Double-Check
                if (item != null)
                {
                    if (item.IsValueCreated)

                        Debug.WriteLine(string.Format("Cache hit! => \"{0}\"", key));
                    return item;
                }
            }
            Debug.WriteLine(string.Format("Cache MISS! => \"{0}\"", key));

            item = new Lazy<TItem>(itemCreateFunc, LazyThreadSafetyMode.ExecutionAndPublication);
            Cache.Set(key, item);
            return item;
        }
    }
}
