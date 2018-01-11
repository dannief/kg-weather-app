using DNTScheduler.Core;
using KG.Weather.Config;
using KG.Weather.Data;
using KG.Weather.Services;
using KG.Weather.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Logging;
using System;

namespace KG.Weather
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Settings = new Settings(configuration);
        }

        public IConfiguration Configuration { get; }

        public Settings Settings { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddDbContext<ApplicationDbContext>(
                options => 
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                    options.EnableSensitiveDataLogging();
                });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMemoryCache();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq();
            });

            services.AddDNTScheduler(options =>
            {
                options.AddScheduledTask<SendNotifcationToWorkersTask>(utcNow =>
                {
                    var localTime = utcNow.ToLocalTime().TimeOfDay;
                    return localTime.Hours == 3 && localTime.Minutes == 55 && localTime.Seconds == 10;
                });
            });

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddSingleton<ISystemClock, SystemClock>();
            services.AddSingleton<APIXULib.IRepository, APIXULib.Repository>();            
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<IWorkerNotificationService, WorkerNotificationService>();
            services.AddSingleton(Settings.ApixuApi);
            services.AddSingleton(Settings.SmtpServer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            app.UseDNTScheduler();

            new Seeder(dbContext, userManager, roleManager).SeedDb().Wait();
        }
    }
}
