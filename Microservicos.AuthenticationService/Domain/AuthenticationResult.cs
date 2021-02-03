using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microservicos.AuthenticationService.Domain
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> Errors { get; set; }
    }
}
