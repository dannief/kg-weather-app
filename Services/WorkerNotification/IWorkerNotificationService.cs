using System.Threading.Tasks;

namespace KG.Weather.Services
{
    public interface IWorkerNotificationService
    {
        Task NotifyWorkers();
    }
}