using System.Collections.Generic;
using System.Threading.Tasks;
using KG.Weather.Data.Models;

namespace KG.Weather.Services
{
    public interface IWeatherService
    {
        Task<List<Forecast>> GetForecast(IEnumerable<string> cities, int numDays);

        Task<Forecast> GetHistory(string city, int lastNumberOfDays);

        Task<HistoricalWeatherData> GetHistoricalGraphData(string cityName, int lastNumberOfDays);

        Task<List<RainForecast>> GetRainForecastForTomorrow(IEnumerable<string> cities);
    }
}