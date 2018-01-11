using Microsoft.Extensions.Configuration;

namespace KG.Weather.Config
{
    public class Settings
    {
        private readonly IConfiguration configuration;

        public Settings(IConfiguration configuration)
        {
            this.configuration = configuration;

            ApixuApi = configuration.GetSection("ApixuApi").Get<ApixuApiSettings>();

            SmtpServer = configuration.GetSection("SmtpServer").Get<SmtpServerSettings>();
        }

        public ApixuApiSettings ApixuApi { get; }

        public SmtpServerSettings SmtpServer { get; }
    }
}
