using Func.Notification;
using Func.Notification.Configuration;
using Func.Notification.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

[assembly: WebJobsStartup(typeof(Startup))]
namespace Func.Notification
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var smtpSettings = new SmtpSettings
            {
                SmtpUsername = config["SmtpSettings:Username"],
                SmtpPassword = config["SmtpSettings:Password"]
            };

            builder.Services.AddSingleton(smtpSettings);
            builder.Services.AddScoped<IMailService, MailService>();
        }
    }
}
