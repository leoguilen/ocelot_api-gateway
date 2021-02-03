using FluentValidation;

namespace Microservicos.AuthenticationService.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email address");

            RuleFor(x => x.Token)
                .NotNull()
                .NotEmpty()
                .WithMessage("Token can't be null or empty");
        }
    }
}
