namespace KG.Weather.Services.Weather.Model
{
    public class RainForecast
    {
        public string City { get; set; }

        public bool IsRainForecasted { get; set; }

        public string Summary { get; set; }

        public string IconUrl { get; set; }
    }
}
