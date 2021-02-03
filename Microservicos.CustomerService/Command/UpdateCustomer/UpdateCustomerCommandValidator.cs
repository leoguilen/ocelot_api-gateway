using FluentValidation;

namespace Microservicos.CustomerService.Command.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
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
        }
    }
}
