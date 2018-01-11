// Code taken from https://github.com/apixu/apixu-csharp
// Modified to be non blocking and handle historical data requests

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace APIXULib
{
    public interface IRepository
    {
        Task<WeatherModel> GetWeatherData(string key, GetBy getBy, string value, Days ForecastOfDays );
        Task<WeatherModel> GetWeatherDataByLatLong( string key, string latitude, string longitude, Days ForecastOfDays);
        Task<WeatherModel> GetWeatherDataByAutoIP(string key, Days ForecastOfDays);

        Task<WeatherModel> GetWeatherData(string key, GetBy getBy, string value);
        Task<WeatherModel> GetWeatherDataByLatLong( string key, string latitude, string longitude);
        Task<WeatherModel> GetWeatherDataByAutoIP(string key);

        Task<WeatherModel> GetWeatherData(string key, GetBy getBy, string value, DateTime date);
        Task<WeatherModel> GetWeatherDataByLatLong(string key, string latitude, string longitude, DateTime date);
        Task<WeatherModel> GetWeatherDataByAutoIP(string key, DateTime date);



    }
    public class Repository : IRepository
    {
        private static HttpClient httpClient;

        private string APIURL = "http://api.apixu.com/v1";
        
        static Repository()
        {
            httpClient = new HttpClient();

            // Add an Accept header for JSON format.
            httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Get Weather Forecast Data

        public Task<WeatherModel> GetWeatherData(string key, GetBy getBy, string value, Days ForecastOfDays)
        {
            return GetData(APIURL +RequestBuilder.PrepareRequest(MethodType.Forecast, key, getBy, value, ForecastOfDays));
        }
      

        public Task<WeatherModel> GetWeatherDataByLatLong(string key, string latitude, string longitude, Days ForecastOfDays)
        {
            return GetData(APIURL + RequestBuilder.PrepareRequestByLatLong(MethodType.Forecast, key,latitude,longitude, ForecastOfDays));

        }

        public Task<WeatherModel> GetWeatherDataByAutoIP(string key, Days ForecastOfDays)
        {
            return GetData(APIURL + RequestBuilder.PrepareRequestByAutoIP(MethodType.Forecast, key, ForecastOfDays));

        }

        #endregion

        #region Get Weather Current Data

        public Task<WeatherModel> GetWeatherData(string key, GetBy getBy, string value )
        {
            return GetData(APIURL + RequestBuilder.PrepareRequest(MethodType.Current, key, getBy, value));
        }


        public Task<WeatherModel> GetWeatherDataByLatLong(string key, string latitude, string longitude)
        {
            return GetData(APIURL + RequestBuilder.PrepareRequestByLatLong(MethodType.Current, key, latitude, longitude));

        }

        public Task<WeatherModel> GetWeatherDataByAutoIP(string key)
        {
            return GetData(APIURL + RequestBuilder.PrepareRequestByAutoIP(MethodType.Current, key));

        }

        #endregion

        #region Get Weather History Data

        public Task<WeatherModel> GetWeatherData(string key, GetBy getBy, string value, DateTime date)
        {
            return GetData(APIURL + RequestBuilder.PrepareRequest(MethodType.History, key, getBy, value, date));
        }


        public Task<WeatherModel> GetWeatherDataByLatLong(string key, string latitude, string longitude, DateTime date)
        {
            return GetData(APIURL + RequestBuilder.PrepareRequestByLatLong(MethodType.History, key, latitude, longitude, date));

        }

        public Task<WeatherModel> GetWeatherDataByAutoIP(string key, DateTime date)
        {
            return GetData(APIURL + RequestBuilder.PrepareRequestByAutoIP(MethodType.History, key, date));

        }

        #endregion

        private async Task<WeatherModel> GetData(string url)
        {            
            using (HttpResponseMessage response = await httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    return JsonConvert.DeserializeObject<WeatherModel>(await response.Content.ReadAsStringAsync());

                }
                else
                {
                    return new WeatherModel();
                } 
            }
        }
    }
}
