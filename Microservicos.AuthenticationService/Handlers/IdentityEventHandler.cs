using MediatR;
using Microservicos.AuthenticationService.Events;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicos.AuthenticationService.Handlers
{
    public class IdentityEventHandler :
        INotificationHandler<RegisteredEvent>,
        INotificationHandler<RecoveredPasswordEvent>
    {
        private readonly string _funcUri;

        public IdentityEventHandler(IConfiguration configuration)
        {
            _funcUri = configuration?["FunctionUri"];
        }

        public async Task Handle(RegisteredEvent notification, CancellationToken cancellationToken)
        {
            using var client = new HttpClient();

            var json = JsonConvert.SerializeObject(notification);
            var content = new StringContent(json);

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = content,
                RequestUri = new Uri($"{_funcUri}api/registered-mail")
            };

            await client.SendAsync(httpRequestMessage);
        }

        public async Task Handle(RecoveredPasswordEvent notification, CancellationToken cancellationToken)
        {
            using var client = new HttpClient();

            var json = JsonConvert.SerializeObject(notification);
            var content = new StringContent(json);

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = content,
                RequestUri = new Uri($"{_funcUri}api/reset-pass-mail")
            };

            await client.SendAsync(httpRequestMessage);
        }
    }
}
