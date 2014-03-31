namespace MWC3.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    
    using MWC3.DAL;
    using MWC3.Models;

    public static class CacheRepository
    {
        public static readonly ApplicationDbContext Db = new ApplicationDbContext();
        public static IEnumerable<Country> GetCountries()
        {
            var key = CacheKey.Countries.ToString();
            var data = MemoryCache.Default.Get(key) as IEnumerable<Country>;
            
            if (data != null)
            {
                return data;
            }

            data = Db.Countries.OrderBy(c => c.Name).ToList();
            MemoryCache.Default.Add(key, data, DateTimeOffset.Now.AddHours(1));
            return data;
        }

        public static IEnumerable<Region> GetRegions()
        {
            var key = CacheKey.Regions.ToString();
            var data = MemoryCache.Default.Get(key) as IEnumerable<Region>;

            if (data != null)
            {
                return data;
            }
            
            data = Db.Regions.OrderBy(c => c.Name).ToList();
            MemoryCache.Default.Add(key, data, DateTimeOffset.Now.AddHours(1));
            return data;
        }

        public static void ClearCountryCache()
        {
            var key = CacheKey.Countries.ToString();
            ClearCache(key);
        }

        public static void ClearRegionCache()
        {
            var key = CacheKey.Regions.ToString();
            ClearCache(key);
        }

        private static void ClearCache(string key)
        {
            MemoryCache.Default.Remove(key);
        }

        private enum CacheKey
        {
            Countries,
            Regions
        }

    }


}