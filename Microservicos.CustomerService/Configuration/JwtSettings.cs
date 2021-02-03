using System;

namespace Microservicos.CustomerService.Configuration
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifetime { get; set; }
        public string Aud { get; set; }
    }
}
