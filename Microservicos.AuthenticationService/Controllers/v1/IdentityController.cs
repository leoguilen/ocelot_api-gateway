using MediatR;
using Microservicos.AuthenticationService.Commands.ConfirmEmail;
using Microservicos.AuthenticationService.Commands.ForgotPasswordRecovery;
using Microservicos.AuthenticationService.Commands.Login;
using Microservicos.AuthenticationService.Commands.Register;
using Microservicos.AuthenticationService.Commands.ResetPassword;
using Microservicos.AuthenticationService.Constants;
using Microservicos.AuthenticationService.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microservicos.AuthenticationService.Controllers.v1
{
    [ApiController]
    [AllowAnonymous]
    [Produces("application/json")]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Registrar usuário no sistema
        /// </summary>
        /// <param name="request">Dados do novo usuário a ser criado</param>
        /// <response code="200">Usuário registrado com sucesso</response>
        /// <response code="400">Um erro ocorreu na validação ao registrar usuário</response>
        [HttpPost(ApiRoutes.Identity.Register)]
        [ProducesResponseType(200, Type = typeof(AuthenticationResult))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Register([FromBody] RegisterCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Success is false)
                return BadRequest(new
                {
                    result.Message,
                    result.Errors
                });

            return Ok(result);
        }

        /// <summary>
        /// Logar usuário no sistema
        /// </summary>
        /// <param name="request">Dados de autenticação de um usuário registrado</param>
        /// <response code="200">Usuário logado com sucesso</response>
        /// <response code="400">Um erro ocorreu no login</response>
        [HttpPost(ApiRoutes.Identity.Login)]
        [ProducesResponseType(200, Type = typeof(AuthenticationResult))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Success is false)
                return BadRequest(new
                {
                    result.Message,
                    result.Errors
                });

            return Ok(result);
        }

        /// <summary>
        /// Confirmar email de usuário
        /// </summary>
        /// <param name="request">Dados para confirmação (Token) do email de um usuário</param>
        /// <response code="200">Email do usuário confirmado com sucesso</response>
        /// <response code="400">Um erro ocorreu ao confirmar email do usuário</response>
        [HttpPost(ApiRoutes.Identity.ConfirmEmail)]
        [ProducesResponseType(200, Type = typeof(AuthenticationResult))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Success is false)
                return BadRequest(new
                {
                    result.Message,
                    result.Errors
                });

            return Ok(result);
        }

        /// <summary>
        /// Resetar senha de um usuário
        /// </summary>
        /// <param name="request">Dados para resetar (Token) senha de um usuário</param>
        /// <response code="200">Senha do usuário resetada com sucesso</response>
        /// <response code="400">Um erro ocorreu ao resetar senha do usuário</response>
        [HttpPost(ApiRoutes.Identity.ResetPassword)]
        [ProducesResponseType(200, Type = typeof(AuthenticationResult))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Success is false)
                return BadRequest(new
                {
                    result.Message,
                    result.Errors
                });

            return Ok(result);
        }

        /// <summary>
        /// Solicitação para recuperar senha de um usuário
        /// </summary>
        /// <param name="request">Dados para solicitar recuperação senha de um usuário</param>
        /// <response code="200">Solicitação confirmada e token gerado com sucesso</response>
        /// <response code="400">Um erro ocorreu ao solicitar a recuperação da senha de um usuário</response>
        [HttpPost(ApiRoutes.Identity.ForgotPassword)]
        [ProducesResponseType(200, Type = typeof(AuthenticationResult))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRecoveryCommand request)
        {
            var result = await _mediator.Send(request);

            if (result.Success is false)
                return BadRequest(new
                {
                    result.Message,
                    result.Errors
                });

            return Ok(result);
        }
    }
}
