using System.Collections.Generic;
using System.Threading.Tasks;
using KG.Weather.Data.Models;

namespace KG.Weather.Services
{
    public interface IWeatherService
    {
        Task<HistoricalWeatherData> GetHistoricalWeatherData(string cityName, int lastNumberOfDays);

        Task<List<RainForecast>> GetRainForecastForTomorrow(IEnumerable<City> cities);
    }
}