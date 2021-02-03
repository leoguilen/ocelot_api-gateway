using Microservicos.AuthenticationService.Commands.Register;
using Swashbuckle.AspNetCore.Filters;

namespace Microservicos.AuthenticationService.Examples
{
    public class RegisterExample : IExamplesProvider<RegisterCommand>
    {
        public RegisterCommand GetExamples()
        {
            return new RegisterCommand
            {
                Name = "Example",
                LastName = "Register",
                Email = "example_register@email.com",
                PhoneNumber = "(11)1020-3040",
                UserName = "example.register",
                Password = "Example123#",
                ConfirmPassword = "Example123#",
                Role = "Operador"
            };
        }
    }
}
