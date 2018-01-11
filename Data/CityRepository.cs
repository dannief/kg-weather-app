using KG.Weather.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KG.Weather.Data
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMemoryCache cache;

        public CityRepository(ApplicationDbContext dbContext, IMemoryCache cache)
        {
            this.dbContext = dbContext;
            this.cache = cache;
        }

        public async Task<List<City>> GetCities()
        {
            var cacheKey = "Cities";

            if(!cache.TryGetValue(cacheKey, out List<City> cities))
            {
                cities = await dbContext.Cities.ToListAsync();

                cache.Set(cacheKey, cities);
            }

            return cities;
        }
    }
}
