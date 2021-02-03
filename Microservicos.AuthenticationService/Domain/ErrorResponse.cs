using System.Collections.Generic;

namespace Microservicos.AuthenticationService.Domain
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
