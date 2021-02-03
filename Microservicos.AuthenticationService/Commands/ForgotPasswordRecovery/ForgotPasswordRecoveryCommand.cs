using MediatR;
using Microservicos.AuthenticationService.Domain;

namespace Microservicos.AuthenticationService.Commands.ForgotPasswordRecovery
{
    public class ForgotPasswordRecoveryCommand : IRequest<AuthenticationResult>
    {
        public string Email { get; set; }
    }
}
