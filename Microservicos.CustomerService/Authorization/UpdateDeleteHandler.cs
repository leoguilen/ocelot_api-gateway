using Microservicos.CustomerService.Constants;
using Microservicos.CustomerService.Extensions;
using Microservicos.CustomerService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microservicos.CustomerService.Authorization
{
    public class UpdateDeleteHandler : AuthorizationHandler<UpdateDeleteRequirement>
    {
        private readonly ICustomerService _service;
        private readonly HttpContext _httpContext;

        public UpdateDeleteHandler(ICustomerService service,
            IHttpContextAccessor accessor)
        {
            _service = service
                ?? throw new ArgumentNullException(nameof(service));
            _httpContext = accessor.HttpContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UpdateDeleteRequirement requirement)
        {
            if (context.User == null)
                context.Fail();

            var userRole = context.User.Claims
                .Single(x => x.Type == ClaimTypes.Role).Value;

            if (context.User.IsInRole(Roles.VISUALIZADOR))
                context.Fail();
            else if (context.User.IsInRole(Roles.ADMINISTRADOR))
                context.Succeed(requirement);
            else
            {
                var userId = Guid.Parse(_httpContext.GetUserId());
                var customerId = (string)_httpContext.Request.RouteValues["Id"];

                var customer = await _service.GetCustomer(Guid.Parse(customerId));

                if (customer == null) context.Fail();
                if (customer.CreatorId != userId) context.Fail();

                context.Succeed(requirement);
            }
        }
    }
}
