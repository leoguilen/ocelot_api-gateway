using Func.Notification.Models;
using Func.Notification.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Func.Notification.Functions
{
    public class ResetPassMailFunction
    {
        private readonly IMailService _mailService;

        public ResetPassMailFunction(IMailService mailService)
        {
            _mailService = mailService
                ?? throw new ArgumentNullException(nameof(mailService));
        }

        [FunctionName("reset-pass-mail")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processing a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation("Request body: {0}", requestBody);
            var data = JsonConvert.DeserializeObject<MailEventModel>(requestBody);

            var response = await _mailService
                .SendMail(data, "resetPass-mail");

            log.LogInformation(response);
            return new OkObjectResult(response);
        }
    }
}
