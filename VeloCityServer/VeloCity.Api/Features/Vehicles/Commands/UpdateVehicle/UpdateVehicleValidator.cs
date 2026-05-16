using FluentValidation;

namespace VeloCity.Api.Features.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleValidator : AbstractValidator<UpdateVehicleCommand>
{
    public UpdateVehicleValidator()
    {
        RuleFor(x => x.SideNumber)
            .NotEmpty().WithMessage("Side number is required")
            .MaximumLength(50).WithMessage("Side number must not exceed 50 characters");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required")
            .MaximumLength(50).WithMessage("Model must not exceed 50 characters");
    }
}
