using FluentValidation;

namespace VeloCity.Api.Features.Users.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is not valid");
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8).WithMessage("Password must have at least 8 characters")
            .Matches("[A-Z]").WithMessage("Password must contain upper case letter")
            .Matches("[0-9]").WithMessage("Password must contain number");
    }
}
