using FluentValidation;
using Microservicos.AuthenticationService.Constants;
using System.Text.RegularExpressions;

namespace Microservicos.AuthenticationService.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name is required");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name can't be empty");
            RuleFor(x => x.Name)
                .MinimumLength(4)
                .WithMessage("Name must contain more than 4 characters");

            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("LastName is required");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName can't be empty");
            RuleFor(x => x.LastName)
                .MinimumLength(4)
                .WithMessage("LastName must contain more than 4 characters");

            RuleFor(x => x.UserName)
                .NotNull()
                .WithMessage("UserName is required");
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("UserName can't be empty");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email address");

            RuleFor(x => x.PhoneNumber)
                .Must(phone => Regex.IsMatch(phone, RegexPattern.PHONE_NUMBER_PATTERN))
                .WithMessage("Phone number has invalid format");

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
