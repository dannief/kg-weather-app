using KG.Weather.Services;
using KG.Weather.Services.Weather.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KG.Weather.Features.Weather
{
    public class GetHistory
    {
        public class Query : IRequest<Result>
        {
            public string City { get; set; }

            public int NumberOfDays { get; set; }
        }

        public class Result
        {
            public Forecast Data { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly IWeatherService weatherService;

            public Handler(IWeatherService weatherService)
            {
                this.weatherService = weatherService;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await weatherService.GetHistory(request.City, request.NumberOfDays);

                return new Result
                {
                    Data = result
                };
            }
        }
    }
}
