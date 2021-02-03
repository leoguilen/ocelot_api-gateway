using MediatR;
using Microservicos.CustomerService.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicos.CustomerService.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        private readonly IHttpContextAccessor _accessor;
        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor 
                ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var userId = _accessor.HttpContext.GetUserId();

            //Request
            _logger.LogInformation($"Handling {typeof(TRequest).Name} (UserId: {userId})");

            Type myType = request.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(request, null);
                _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
            }

            var response = await next();

            //Response
            _logger.LogInformation($"Handled {typeof(TResponse).Name} (UserId: {userId})");

            return response;
        }
    }
}
