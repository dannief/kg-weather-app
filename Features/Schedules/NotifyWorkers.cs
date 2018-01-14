using KG.Weather.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace KG.Weather.Features.Schedules
{
    public class NotifyWorkers
    {
        public class Command : IRequest
        {
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IWorkerNotificationService workerNotificationService;

            public Handler(IWorkerNotificationService workerNotificationService)
            {
                this.workerNotificationService = workerNotificationService;
            }

            public Task Handle(Command message, CancellationToken cancellationToken)
            {
                return workerNotificationService.NotifyWorkers();
            }
        }
    }
}
