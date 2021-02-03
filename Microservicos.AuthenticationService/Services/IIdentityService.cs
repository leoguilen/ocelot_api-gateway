using Microservicos.AuthenticationService.Commands.ConfirmEmail;
using Microservicos.AuthenticationService.Commands.ForgotPasswordRecovery;
using Microservicos.AuthenticationService.Commands.Login;
using Microservicos.AuthenticationService.Commands.Register;
using Microservicos.AuthenticationService.Commands.ResetPassword;
using Microservicos.AuthenticationService.Domain;
using System;
using System.Threading.Tasks;

namespace Microservicos.AuthenticationService.Services
{
    public interface IIdentityService
    {
        Task<Tuple<AuthenticationResult, string>> RegisterAsync(RegisterCommand command);
        Task<AuthenticationResult> LoginAsync(LoginCommand command);
        Task<Tuple<AuthenticationResult, string>> ForgotPasswordRecoveryAsync(ForgotPasswordRecoveryCommand command);
        Task<AuthenticationResult> ConfirmEmailAsync(ConfirmEmailCommand command);
        Task<AuthenticationResult> ResetPasswordAsync(ResetPasswordCommand command);
    }
}
