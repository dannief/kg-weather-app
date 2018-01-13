using System.Collections.Generic;
using System.Linq;

namespace KG.Weather.Services
{
    public class Forecast
    { 
        public string City { get; set; }

        public List<ForecastDay> Days { get; set; } = new List<ForecastDay>();

        public ForecastDay Tomorrow => Days?.Count > 1 ? Days[1] : null;
    }
}
