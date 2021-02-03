using FluentValidation;

namespace Microservicos.AuthenticationService.Commands.ForgotPasswordRecovery
{
    public class ForgotPasswordRecoveryCommandValidator : AbstractValidator<ForgotPasswordRecoveryCommand>
    {
        public ForgotPasswordRecoveryCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email address");
        }
    }
}
