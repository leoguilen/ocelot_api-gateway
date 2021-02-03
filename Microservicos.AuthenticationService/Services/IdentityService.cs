using Microservicos.AuthenticationService.Commands.ConfirmEmail;
using Microservicos.AuthenticationService.Commands.ForgotPasswordRecovery;
using Microservicos.AuthenticationService.Commands.Login;
using Microservicos.AuthenticationService.Commands.Register;
using Microservicos.AuthenticationService.Commands.ResetPassword;
using Microservicos.AuthenticationService.Configuration;
using Microservicos.AuthenticationService.Domain;
using Microservicos.AuthenticationService.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservicos.AuthenticationService.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings,
            ILogger<IdentityService> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<AuthenticationResult> ConfirmEmailAsync(ConfirmEmailCommand command)
        {
            var user = await _userManager
                .FindByEmailAsync(command.Email);

            if (user is null)
            {
                _logger.LogWarning(
                    "Nenhum usuário foi encontrado que corresponda ao email '{0}'",
                    command.Email);

                return new AuthenticationResult
                {
                    Message = "Não existe usuário com email especificado",
                    Success = false
                };
            }

            var confirmedEmailResult = await _userManager
                .ConfirmEmailAsync(user, command.Token);

            if (!confirmedEmailResult.Succeeded)
            {
                _logger.LogWarning(
                    "Ocorreu uma falha ao confirmar email do usuário. Erros: {0}",
                    JsonSerializer.Serialize(confirmedEmailResult.Errors));

                return new AuthenticationResult
                {
                    Message = "Falha no confirmação de email do usuário",
                    Success = false,
                    Errors = confirmedEmailResult.Errors.Select(x => x.Description).ToArray()
                };
            }

            _logger.LogInformation("Email {0} confirmado com sucesso", command.Email);

            return new AuthenticationResult
            {
                Message = "Email confirmado com sucesso",
                Success = true
            };
        }

        public async Task<Tuple<AuthenticationResult, string>> ForgotPasswordRecoveryAsync(ForgotPasswordRecoveryCommand command)
        {
            var user = await _userManager
                .FindByEmailAsync(command.Email);

            if (user is null)
            {
                _logger.LogWarning(
                    "Nenhum usuário com email {0} foi encontrado",
                    command.Email);

                return Tuple.Create(
                    new AuthenticationResult
                    {
                        Message = "Não foi encontrado usuário com email especificado",
                        Success = false
                    }, string.Empty);
            }

            var resetPasswordToken = await _userManager
                .GeneratePasswordResetTokenAsync(user);

#if DEBUG
            _logger.LogInformation("Token gerado: {0}", resetPasswordToken);
#endif

            _logger.LogInformation(
                "Token para resetar a senha do email {0} gerado",
                command.Email);

            return Tuple.Create(
                    new AuthenticationResult
                    {
                        Message = "Token para resetar a senha gerado. Uma notificação será enviada com o token.",
                        Success = true,
                    }, resetPasswordToken);
        }

        public async Task<AuthenticationResult> LoginAsync(LoginCommand command)
        {
            var user = await _userManager
                .FindByEmailAsync(command.Email);

            if (user is null)
            {
                _logger.LogWarning(
                    "Nenhum usuário com email {0} foi encontrado",
                    command.Email);

                return new AuthenticationResult
                {
                    Message = "Não foi encontrado usuário com email especificado",
                    Success = false
                };
            }

            var confirmedEmail = await _userManager.IsEmailConfirmedAsync(user);

            if (!confirmedEmail)
            {
                _logger.LogWarning(
                    "Login recusado, pois o email {0} não foi confirmado",
                    command.Email);

                return new AuthenticationResult
                {
                    Message = "Usuário não pode logar no sistema, pois não está com email confirmado",
                    Success = false
                };
            }

            var loginResult = await _signInManager
                .PasswordSignInAsync(user, command.Password, true, lockoutOnFailure: true);

            if (loginResult.IsLockedOut)
            {
                _logger.LogWarning(
                    "Login recusado, pois o usuário com email {0} está bloqueado",
                    command.Email);

                return new AuthenticationResult
                {
                    Message = "Usuário encontra-se bloqueado",
                    Success = false
                };
            }

            if (!loginResult.Succeeded)
            {
                _logger.LogWarning(
                    "Falha ao logar usuário com email {0}",
                    command.Email);

                return new AuthenticationResult
                {
                    Message = "Falha no login",
                    Success = false
                };
            }

            _logger.LogInformation(
                "Usuário (Email = {0}) autenticado com sucesso",
                command.Email);

            var authResult = await TokenHelper
                .GenerateAuthenticationResultForUserAsync(
                user, _jwtSettings.Value, _userManager);
            authResult.Message = "Login efetuado com sucesso";

            return authResult;
        }

        public async Task<Tuple<AuthenticationResult, string>> RegisterAsync(RegisterCommand command)
        {
            var user = await _userManager
                .FindByEmailAsync(command.Email);

            if (!(user is null))
            {
                _logger.LogWarning(
                    "Foi encontrado um usuário com o mesmo endereço de email ({0}) já cadastrado",
                    command.Email);

                return Tuple.Create(
                    new AuthenticationResult
                    {
                        Message = "Falha no registro de usuário",
                        Success = false,
                        Errors = new[] { "Já existe usuário com esse email" }
                    }, string.Empty);
            }


            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Name = command.Name,
                LastName = command.LastName,
                UserName = command.UserName,
                PhoneNumber = command.PhoneNumber,
                Email = command.Email
            };

            var createdUser = await _userManager
                .CreateAsync(newUser, command.Password);

            if (!createdUser.Succeeded)
            {
                _logger.LogWarning(
                    "Ocorreu uma falha ao registrar usuário. Erros: {0}",
                    JsonSerializer.Serialize(createdUser.Errors));

                return Tuple.Create(
                    new AuthenticationResult
                    {
                        Message = "Falha no registro de usuário",
                        Success = false,
                        Errors = createdUser.Errors.Select(x => x.Description).ToArray()
                    }, string.Empty);
            }

            await _userManager.AddToRoleAsync(newUser, command.Role);

            var confirmEmailToken = await _userManager
                .GenerateEmailConfirmationTokenAsync(newUser);

#if DEBUG
            _logger.LogInformation("Token gerado: {0}", confirmEmailToken);
#endif

            _logger.LogInformation(
                "Token para confirmação do email {0} gerado",
                newUser.Email, confirmEmailToken);
            _logger.LogInformation(
                "Usuário registrado com sucesso");

            var authResult = await TokenHelper
                .GenerateAuthenticationResultForUserAsync(
                newUser, _jwtSettings.Value, _userManager);
            authResult.Message = "Registro efetuado com sucesso";

            return Tuple.Create(authResult, confirmEmailToken);
        }

        public async Task<AuthenticationResult> ResetPasswordAsync(ResetPasswordCommand command)
        {
            var user = await _userManager
                .FindByEmailAsync(command.Email);

            if (user is null)
            {
                _logger.LogWarning(
                    "Nenhum usuário com email {0} foi encontrado",
                    command.Email);

                return new AuthenticationResult
                {
                    Message = "Não existe usuário com email especificado",
                    Success = false
                };
            }

            var resetPasswordResult = await _userManager
                .ResetPasswordAsync(user, command.Token, command.Password);

            if (!resetPasswordResult.Succeeded)
            {
                _logger.LogWarning(
                    "Ocorreu uma falha ao resetar senha do usuário com email {0}. Erros: {1}",
                    command.Email, JsonSerializer.Serialize(resetPasswordResult.Errors));

                return new AuthenticationResult
                {
                    Message = "Falha ao resetar senha",
                    Success = false,
                    Errors = resetPasswordResult.Errors.Select(x => x.Description).ToArray()
                };
            }

            _logger.LogInformation(
                "Senha do usuário (Email = {0}) resetada com sucesso",
                command.Email);

            return new AuthenticationResult
            {
                Message = "Senha resetada com sucesso",
                Success = true
            };
        }
    }
}
