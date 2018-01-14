using System.Threading.Tasks;
using KG.Weather.Features.Weather;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KG.Weather.Controllers
{
    [Produces("application/json")]
    [Route("v1/weather")]
    public class WeatherController : Controller
    {
        private readonly IMediator mediator;

        public WeatherController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("forecast")]
        public async Task<IActionResult> GetForecast([FromQuery]GetForecast.Query query)
        {
            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery]GetHistory.Query query)
        {
            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}