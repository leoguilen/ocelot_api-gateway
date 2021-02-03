using Newtonsoft.Json;

namespace Func.Notification.Models
{
    public class MailEventModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fullname")]
        public string FullName { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
