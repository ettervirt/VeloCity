using FluentValidation;

namespace VeloCity.Api.Features.Users.Commands.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Current password is empty");
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(8).WithMessage("Password must have at least 8 characters")
            .Matches("[A-Z]").WithMessage("Password must contain upper case letter")
            .Matches("[0-9]").WithMessage("Password must contain number");
    }
}
