using System.Collections.Generic;
using System.Threading.Tasks;
using KG.Weather.Data.Models;

namespace KG.Weather.Data
{
    public interface ICityRepository
    {
        Task<List<City>> GetCities();
    }
}