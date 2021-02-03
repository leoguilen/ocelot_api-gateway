using MediatR;
using Newtonsoft.Json;

namespace Microservicos.AuthenticationService.Events
{
    public class RegisteredEvent : INotification
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("fullname")]
        public string FullName { get; set; }
        
        [JsonProperty("token")]
        public string ConfirmationEmailToken { get; set; }
    }
}
