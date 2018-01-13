using KG.Weather.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KG.Weather.Data
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMemoryCache cache;

        public WorkerRepository(ApplicationDbContext dbContext, IMemoryCache cache)
        {
            this.dbContext = dbContext;
            this.cache = cache;
        }

        public async Task<List<Worker>> GetWorkers()
        {
            var cacheKey = "Workers";

            if (!cache.TryGetValue(cacheKey, out List<Worker> workers))
            {
                workers = await dbContext.Workers.Include(x => x.City).ToListAsync();

                cache.Set(cacheKey, workers);
            }

            return workers;
        }
    }
}
