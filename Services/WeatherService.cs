using APIXULib;
using KG.Weather.Config;
using KG.Weather.Data.Models;
using Microsoft.Extensions.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace KG.Weather.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ISystemClock clock;
        private readonly APIXULib.IRepository apixu;
        private readonly ApixuApiSettings apiSettings;

        public WeatherService(ISystemClock clock, APIXULib.IRepository apixu, ApixuApiSettings apiSettings)
        {
            this.clock = clock;
            this.apixu = apixu;
            this.apiSettings = apiSettings;
        }

        public async Task<List<RainForecast>> GetRainForecastForTomorrow(IEnumerable<City> cities)
        {
            var forcasts =
                (
                    await Task.WhenAll(
                        cities.Select(city => apixu.GetWeatherData(apiSettings.Key, GetBy.CityName, city.FullName, Days.Two)))
                )
                .Select(weatherModel =>
                {
                    var cityId =
                        cities.Single(c => c.Name == weatherModel.location.name && c.Country == weatherModel.location.country).Id;

                    var forcastInfo = weatherModel.forecast.forecastday[1].day;

                    return new RainForecast
                    {
                        CityId = cityId,
                        IsRainForecasted = forcastInfo.totalprecip_in > 0,
                        Summary = forcastInfo.condition.text,
                        ImageUrl = forcastInfo.condition.icon
                    };
                    
                })
                .ToList();

            return forcasts;                
        }

        public async Task<HistoricalWeatherData> GetHistoricalWeatherData(string cityName, int lastNumberOfDays)
        {
            if (lastNumberOfDays > 7) lastNumberOfDays = 7;

            var historicalData =
                (
                    await Task.WhenAll(
                        Enumerable.Range(1, lastNumberOfDays)
                        .Reverse()
                        .Select(i => clock.UtcNow.AddDays(-i).Date)
                        .Select(date => apixu.GetWeatherData(apiSettings.Key, GetBy.CityName, cityName, date)))
                )
                .Select(weatherModel =>
                {
                    var forecast = weatherModel.forecast.forecastday[0];
                    return new
                    {
                        Date = DateTime.ParseExact(forecast.date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        Precipitation = forecast.day.totalprecip_mm,
                        Temperature = forecast.day.avgtemp_c
                    };
                })
                .Aggregate(
                    new HistoricalWeatherData(),
                    (data, next) =>
                    {
                        data.Precipitation.Add(next.Date, next.Precipitation);
                        data.Temperature.Add(next.Date, next.Temperature);

                        return data;
                    });

            return historicalData;                
        }
    }

    public class HistoricalWeatherData
    {
        public IDictionary<DateTime, double> Precipitation { get; set; }
            = new Dictionary<DateTime, double>();

        public IDictionary<DateTime, double> Temperature { get; set; }
        = new Dictionary<DateTime, double>();
    }

    public class RainForecast
    {
        public int CityId { get; set; }

        public bool IsRainForecasted { get; set; }

        public string Summary { get; set; }

        public string ImageUrl { get; set; }
    }
}
