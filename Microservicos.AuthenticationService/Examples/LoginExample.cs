using Microservicos.AuthenticationService.Commands.Login;
using Swashbuckle.AspNetCore.Filters;

namespace Microservicos.AuthenticationService.Examples
{
    public class LoginExample : IExamplesProvider<LoginCommand>
    {
        public LoginCommand GetExamples()
        {
            return new LoginCommand
            {
                Email = "example_register@email.com",
                Password = "Example123#",
            };
        }
    }
}
