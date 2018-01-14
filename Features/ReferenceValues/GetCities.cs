using KG.Weather.Data;
using KG.Weather.Data.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KG.Weather.Features.ReferenceValues
{
    public class GetCities
    {
        public class Query : IRequest<Result>
        {

        }

        public class Result
        {
            public List<City> Data { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly ICityRepository cityRepository;

            public Handler(ICityRepository cityRepository)
            {
                this.cityRepository = cityRepository;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var cities = await cityRepository.GetCities();

                return new Result
                {
                    Data = cities
                };
            }
        }
    }
}
