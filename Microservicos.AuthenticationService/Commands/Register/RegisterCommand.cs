using MediatR;
using Microservicos.AuthenticationService.Domain;

namespace Microservicos.AuthenticationService.Commands.Register
{
    public class RegisterCommand : IRequest<AuthenticationResult>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}
