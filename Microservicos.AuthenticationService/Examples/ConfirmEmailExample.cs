using Microservicos.AuthenticationService.Commands.ConfirmEmail;
using Swashbuckle.AspNetCore.Filters;

namespace Microservicos.AuthenticationService.Examples
{
    public class ConfirmEmailExample : IExamplesProvider<ConfirmEmailCommand>
    {
        public ConfirmEmailCommand GetExamples()
        {
            return new ConfirmEmailCommand
            {
                Email = "user_email@provider.com",
                Token = "a64ce75969fc8cbd19b9ca51ffd1e3f6b02117f3652d04df6d257014c906d17e"
            };
        }
    }
}
