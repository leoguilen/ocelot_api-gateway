using FluentValidation;
using Microservicos.AuthenticationService.Constants;
using System.Text.RegularExpressions;

namespace Microservicos.AuthenticationService.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email address");

            RuleFor(x => x.Token)
                .NotNull()
                .NotEmpty()
                .WithMessage("Token can't be null or empty");

            RuleFor(x => x.Password)
                .Must(pwd => Regex.IsMatch(pwd, RegexPattern.PASSWORD_PATTERN))
                .WithMessage("Password does not meet the requirements of a strong standard")
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.ConfirmPassword)
                .Equal(prop => prop.Password)
                .WithMessage("Password confirmation is not equal to password")
                .When(x => !string.IsNullOrEmpty(x.Password));
        }
    }
}
