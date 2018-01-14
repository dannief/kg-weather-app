using System.Threading.Tasks;
using KG.Weather.Features.ReferenceValues;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KG.Weather.Controllers
{
    [Produces("application/json")]
    [Route("v1/reference-values")]
    public class ReferenceValuesController : Controller
    {
        private readonly IMediator mediator;

        public ReferenceValuesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            var result = await mediator.Send(new GetCities.Query());

            return Ok(result);
        }
    }
}