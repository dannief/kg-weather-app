using DNTScheduler.Core.Contracts;
using KG.Weather.Services;
using System.Threading.Tasks;

namespace KG.Weather.Tasks
{
    public class SendNotifcationToWorkersTask : IScheduledTask
    {
        private readonly IWorkerNotificationService workerNotificationService;

        public SendNotifcationToWorkersTask(
            IWorkerNotificationService workerNotificationService)
        {           
            this.workerNotificationService = workerNotificationService;
        }

        public bool IsShuttingDown { get; set; }

        public async Task RunAsync()
        {
            if (this.IsShuttingDown)
            {
                return;
            }

            await workerNotificationService.NotifyWorkers();
        }
        
    }
}
