using MediatR;
using Microservicos.AuthenticationService.Commands.ConfirmEmail;
using Microservicos.AuthenticationService.Commands.ForgotPasswordRecovery;
using Microservicos.AuthenticationService.Commands.Login;
using Microservicos.AuthenticationService.Commands.Register;
using Microservicos.AuthenticationService.Commands.ResetPassword;
using Microservicos.AuthenticationService.Domain;
using Microservicos.AuthenticationService.Events;
using Microservicos.AuthenticationService.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicos.AuthenticationService.Handlers
{
    public class IdentityHandler :
        IRequestHandler<RegisterCommand, AuthenticationResult>,
        IRequestHandler<LoginCommand, AuthenticationResult>,
        IRequestHandler<ResetPasswordCommand, AuthenticationResult>,
        IRequestHandler<ForgotPasswordRecoveryCommand, AuthenticationResult>,
        IRequestHandler<ConfirmEmailCommand, AuthenticationResult>
    {
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;

        public IdentityHandler(
            IIdentityService identityService,
            IMediator mediator)
        {
            _mediator = mediator;
            _identityService = identityService
                ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService
                .RegisterAsync(request);

            //await _mediator.Publish(new RegisteredEvent
            //{
            //    Email = request.Email,
            //    FullName = request.Name + request.LastName,
            //    ConfirmationEmailToken = result.Item2
            //});

            return result.Item1;
        }

        public async Task<AuthenticationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.LoginAsync(request);
        }

        public async Task<AuthenticationResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.ResetPasswordAsync(request);
        }

        public async Task<AuthenticationResult> Handle(ForgotPasswordRecoveryCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ForgotPasswordRecoveryAsync(request);

            //await _mediator.Publish(new RecoveredPasswordEvent
            //{
            //    Email = request.Email,
            //    RecoverPasswordToken = result.Item2
            //});

            return result.Item1;
        }

        public async Task<AuthenticationResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.ConfirmEmailAsync(request);
        }
    }
}
