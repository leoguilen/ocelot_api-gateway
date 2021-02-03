using MediatR;
using Microservicos.AuthenticationService.Domain;

namespace Microservicos.AuthenticationService.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<AuthenticationResult>
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
