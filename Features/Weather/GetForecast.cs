using KG.Weather.Data;
using KG.Weather.Services;
using KG.Weather.Services.Weather.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KG.Weather.Features.Weather
{
    public class GetForecast
    {
        public class Query: IRequest<Result>
        {
            public string[] Cities { get; set; }

            public int NumberOfDays { get; set; }
        }

        public class Result
        {
            public List<Forecast> Data { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly IWeatherService weatherService;
            private readonly ICityRepository cityRepository;

            public Handler(IWeatherService weatherService, ICityRepository cityRepository)
            {
                this.weatherService = weatherService;
                this.cityRepository = cityRepository;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var cities = request.Cities;
                var numDays = request.NumberOfDays;

                if (cities == null || !cities.Any())
                {
                    cities = (await cityRepository.GetCities()).Select(c => c.FullName).ToArray();
                }

                if (numDays == 0)
                {
                    numDays = 7;
                }

                var result = await weatherService.GetForecast(cities, numDays);

                return new Result
                {
                    Data = result
                };
            }
        }
    }
}
