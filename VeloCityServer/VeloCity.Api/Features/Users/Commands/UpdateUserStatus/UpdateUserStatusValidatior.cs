using FluentValidation;

namespace VeloCity.Api.Features.Users.Commands.UpdateUserStatus;

public class UpdateUserStatusValidator : AbstractValidator<UpdateUserStatusCommand>
{
    public UpdateUserStatusValidator()
    {
        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Wrong user role");
    }
}
