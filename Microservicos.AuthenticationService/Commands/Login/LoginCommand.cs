using MediatR;
using Microservicos.AuthenticationService.Domain;

namespace Microservicos.AuthenticationService.Commands.Login
{
    public class LoginCommand : IRequest<AuthenticationResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
