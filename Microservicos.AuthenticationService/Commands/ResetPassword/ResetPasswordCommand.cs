using MediatR;
using Microservicos.AuthenticationService.Domain;

namespace Microservicos.AuthenticationService.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<AuthenticationResult>
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
