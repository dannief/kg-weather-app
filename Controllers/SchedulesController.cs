using System.Threading.Tasks;
using KG.Weather.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KG.Weather.Controllers
{
    [Produces("application/json")]
    [Route("v1/schedules")]
    public class SchedulesController : Controller
    {
        private readonly IMediator mediator;

        public SchedulesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("notifications")]
        public async Task<IActionResult> NotifyWorkers()
        {
            await mediator.Send(new NotifyWorkers.Command());

            return Ok();
        }
    }
}