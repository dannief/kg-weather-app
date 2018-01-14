using System;
using System.Collections.Generic;

namespace KG.Weather.Services.Weather.Model
{
    public class HistoricalWeatherData
    {
        public IDictionary<DateTime, double> Precipitation { get; set; }
            = new Dictionary<DateTime, double>();

        public IDictionary<DateTime, double> Temperature { get; set; }
        = new Dictionary<DateTime, double>();
    }
}
