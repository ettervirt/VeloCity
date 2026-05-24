using FluentValidation;

namespace VeloCity.Api.Features.Tickets.Commands.UpdateTicketType;

public class UpdateTicketTypeValidator : AbstractValidator<UpdateTicketTypeCommand>
{
    public UpdateTicketTypeValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Invalid ticket type ID.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.DurationInMinutes)
            .GreaterThanOrEqualTo(0).WithMessage("Duration in minutes must be zero or greater.");

        RuleFor(x => x.ZoneLimit)
            .GreaterThanOrEqualTo(0).WithMessage("Zone limit must be zero or greater.");
    }
}
