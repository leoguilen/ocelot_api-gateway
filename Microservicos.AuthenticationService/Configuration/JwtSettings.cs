using System;

namespace Microservicos.AuthenticationService.Configuration
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifetime { get; set; }
        public string Aud { get; set; }
    }
}
