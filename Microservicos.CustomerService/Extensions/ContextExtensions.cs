using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Microservicos.CustomerService.Extensions
{
    public static class ContextExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;

            return httpContext.User.Claims
                .Single(x => x.Type == "Id").Value;
        }
    }
}
