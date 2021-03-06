﻿using System;
using Microsoft.Extensions.Caching.Memory;

namespace Service.Caching.LocalMemory
{
    /// <summary>
    /// local memory (in-memory) cache implementation class
    /// </summary>
    public class LocalMemoryCacheService : ICacheService
    {
        /// <summary>
        /// local memory cache object
        /// </summary>
        private IMemoryCache _cache;

        #region singleton definition

        private readonly object _padlock = new object();

        public static LocalMemoryCacheService Instance { get; } = new LocalMemoryCacheService();

        private LocalMemoryCacheService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        #endregion singleton definition

        #region ICacheService implementation

        public void Add(string key, object item, int expireInMinutes)
        {
            lock (_padlock)
            {
                if (expireInMinutes == 0)
                {
                    _cache.Set(key, item, DateTimeOffset.MaxValue);
                }
                else
                {
                    var absoluteExpiration = new TimeSpan(0, 0, expireInMinutes, 0);
                    _cache.Set(key, item, DateTimeOffset.Now.Add(absoluteExpiration));
                }
            }
        }

        public T Get<T>(string key)
        {
            lock (_padlock)
            {
                return (T)_cache.Get(key); //_cache[key];
            }
        }

        public void Remove(string key)
        {
            lock (_padlock)
            {
                _cache.Remove(key);
            }
        }

        public void RemoveAll()
        {
            lock (_padlock)
            {
                _cache.Dispose();
                _cache = new MemoryCache(new MemoryCacheOptions());
            }
        }

        #endregion ICacheService implementation
    }
}
