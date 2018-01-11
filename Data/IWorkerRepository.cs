using System.Collections.Generic;
using System.Threading.Tasks;
using KG.Weather.Data.Models;

namespace KG.Weather.Data
{
    public interface IWorkerRepository
    {
        Task<List<Worker>> GetWorkers();
    }
}