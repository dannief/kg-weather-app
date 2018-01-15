using APIXULib;
using KG.Weather.Config;
using KG.Weather.Services.Weather.Model;
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

        public async Task<List<Weather.Model.Forecast>> GetForecast(IEnumerable<string> cities, int numDays)
        {
            if (numDays > 14) numDays = 14;

            var forecasts =
                (
                    await Task.WhenAll(
                        cities.Select(city => apixu.GetWeatherData(apiSettings.Key, GetBy.CityName, city, (Days)numDays)))
                )
                .Select(weatherModel => ToForecastModel(weatherModel))
                .ToList();

            return forecasts;
        }

        public async Task<Weather.Model.Forecast> GetHistory(string city, int lastNumberOfDays)
        {
            if (lastNumberOfDays > 7) lastNumberOfDays = 7;

            var historicalData =
                (
                    await Task.WhenAll(
                        Enumerable.Range(1, lastNumberOfDays)
                        .Reverse()
                        .Select(i => clock.UtcNow.Date.AddDays(-i))
                        .Select(date => apixu.GetWeatherData(apiSettings.Key, GetBy.CityName, city, date)))
                )
                .Select(weatherModel => ToForecastModel(weatherModel))
                .Aggregate(
                    new Weather.Model.Forecast { Location = city },
                    (data, next) =>
                    {
                        data.Days.AddRange(next.Days);
                        return data;
                    });

            return historicalData;
        }

        public async Task<List<RainForecast>> GetRainForecastForTomorrow(IEnumerable<string> cities)
        {
            var forecasts =
                (
                    await GetForecast(cities, 2)
                )
                .Select(forecastModel =>
                {
                    var forecastInfo = forecastModel.Tomorrow;

                    return new RainForecast
                    {
                        City = forecastModel.Location,
                        IsRainForecasted = forecastInfo.IsRainForecasted,
                        Summary = forecastInfo.Summary,
                        IconUrl = forecastInfo.IconUrl
                    };
                    
                })
                .ToList();

            return forecasts;                
        }

        public async Task<HistoricalWeatherData> GetHistoricalGraphData(string cityName, int lastNumberOfDays)
        {
            var historicalData =
                (
                    await GetHistory(cityName, lastNumberOfDays)
                ) 
                .Days
                .Aggregate(
                    new HistoricalWeatherData(),
                    (data, next) =>
                    {
                        data.Precipitation.Add(next.Date, next.Precipitation);
                        data.Temperature.Add(next.Date, next.Temperature.Avg);

                        return data;
                    });

            return historicalData;                
        }

        private Weather.Model.Forecast ToForecastModel(WeatherModel weatherModel)
        {
            var forecastDays = weatherModel.forecast.forecastday.Select(x => new { x.date, x.day });

            return new Weather.Model.Forecast
            {
                City = weatherModel.location.name,
                Location = $"{weatherModel.location.name}, {weatherModel.location.country}",
                Days = forecastDays.Select(forecastInfo => new ForecastDay
                {
                    Date = DateTime.ParseExact(forecastInfo.date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Humidity = forecastInfo.day.avghumidity,
                    IconCode = forecastInfo.day.condition.code,
                    IconUrl = "http:" + forecastInfo.day.condition.icon,
                    IsRainForecasted = forecastInfo.day.totalprecip_in > 0,
                    Summary = forecastInfo.day.condition.text,
                    Temperature = new TemperatureInfo
                    {
                        Min = forecastInfo.day.mintemp_c,
                        Max = forecastInfo.day.maxtemp_c,
                        Avg = forecastInfo.day.avgtemp_c
                    },
                    Wind = forecastInfo.day.maxwind_kph,
                    Precipitation = forecastInfo.day.totalprecip_mm
                }).ToList()
            };
        }
    }
}
