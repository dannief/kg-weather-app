using KG.Weather.Config;
using KG.Weather.Data;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;


namespace KG.Weather.Services
{
    public class WorkerNotificationService : IWorkerNotificationService
    {
        private readonly ILogger<WorkerNotificationService> logger;
        private readonly IWeatherService weatherService;
        private readonly IWorkerRepository workerRepository;
        private readonly ICityRepository cityRepository;
        private readonly SmtpServerSettings smtpServerSettings;


        public WorkerNotificationService(
            ILogger<WorkerNotificationService> logger,
            IWeatherService weatherService,
            IWorkerRepository workerRepository,
            ICityRepository cityRepository,
            SmtpServerSettings smtpServerSettings)
        {
            this.logger = logger;
            this.weatherService = weatherService;
            this.workerRepository = workerRepository;
            this.cityRepository = cityRepository;
            this.smtpServerSettings = smtpServerSettings;
        }


        public async Task NotifyWorkers()
        {
            var cities = (await cityRepository.GetCities()).Select(c => c.FullName);

            var rainForecast = await weatherService.GetRainForecastForTomorrow(cities);

            var workers = await workerRepository.GetWorkers();

            var smtpClient = new SmtpClient(smtpServerSettings.Host, smtpServerSettings.Port);

            workers
                .GroupBy(worker => worker.City.FullName)
                .Select(workerGroup =>
                {
                    var forecastForCity = rainForecast.Single(f => f.City == workerGroup.Key);

                    var numHours = forecastForCity.IsRainForecasted ? 4 : 8;

                    return new
                    {
                        EmailAddresses = workerGroup.Select(w => w.Email).ToList(),
                        Message =
                            $@"<p>Dear Worker,</p>
                            <p>Please note, you are scheduled to work for <strong>{numHours}</strong> hours tomorrow.</p>                            
                            <div style=""text-align:center;width:400px;border:1px solid lightblue;padding:10px"">
                              <p><strong>Forcast for tommorow</strong></p></br>
                              <em>{forecastForCity.Summary}</em>
                              <img src=""http:{forecastForCity.IconUrl}"">
                            </div>
                            <p>Regards,</br></br>Management"
                    };
                })
                .ToList()
                .ForEach(message =>
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpServerSettings.From),
                        Subject = "KG Work Schedule",
                        Body = message.Message,
                        IsBodyHtml = true
                    };
                    message.EmailAddresses.ForEach(address => mailMessage.To.Add(address));

                    smtpClient.Send(mailMessage);
                });
        }
    }
}
