
using System;
using System.Linq;
using System.Reflection;
using Common;
using Microsoft.Extensions.Options;

namespace Service.Caching
{
    public class CacheService : ICacheService
    {
        private readonly ICacheService _cache;

        public CacheService(IOptions<AppSettings> appSettings)
        {
            var cachePrviderType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(p => p.Name == appSettings.Value.CacheProvider);

            if (cachePrviderType == null)
            {
                throw new ApplicationException($"{appSettings.Value.CacheProvider} CacheProviderNotFound");
            }

            _cache = cachePrviderType.GetProperty("Instance").GetValue(null) as ICacheService;
        }

        public void Add(string key, object item, int expireInMinutes)
        {
            _cache.Add(key, item, expireInMinutes);
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveAll()
        {
            _cache.RemoveAll();
        }
    }
}
