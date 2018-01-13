using System;

namespace KG.Weather.Services
{
    public class ForecastDay
    {
        public DateTime Date { get; set; }

        public string Summary { get; set; }

        public int IconCode { get; set; }

        public string IconUrl { get; set; }

        public (double Min, double Max, double Avg) Temperature { get; set; }
        
        public double Wind { get; set; }

        public double Humidity { get; set; }

        public double Precipitation { get; set; }

        public bool IsRainForecasted { get; set; }
    }
}
