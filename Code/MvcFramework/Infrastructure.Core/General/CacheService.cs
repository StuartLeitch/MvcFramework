using System.Linq;
using System;
using System.Diagnostics;
using System.Runtime.Caching;

namespace Infrastructure.Core
{
    public class CacheService<T>
    {
        private static readonly object ThisLock = new object();

        [DebuggerStepThrough]
        public T GetItem(string cacheKey, int cacheDurationMinutes, Func<T> functionToPopulateCache)
        {
            ObjectCache cache = MemoryCache.Default;

            var cacheObject = cache[cacheKey];

            if (cacheObject == null)
            {
                lock (ThisLock)
                {
                    // Check that cache has not been filled while we've been waiting for the lock
                    if (cache[cacheKey] == null)
                    {
                        var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheDurationMinutes) };
                        cache.Add(cacheKey, functionToPopulateCache.Invoke(), policy);
                    }

                    cacheObject = cache[cacheKey];
                }
            }

            return (T)cacheObject;
        }

        public void ClearItem(string cacheKey)
        {
            ObjectCache cache = MemoryCache.Default;

            lock (ThisLock)
            {
                cache.Remove(cacheKey);
            }
        }
    }
}