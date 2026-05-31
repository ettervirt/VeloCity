using FluentValidation;

namespace VeloCity.Api.Features.Tickets.Commands.ValidateTicket;

public class ValidateTicketValidator : AbstractValidator<ValidateTicketCommand>
{
    public ValidateTicketValidator()
    {
        RuleFor(x => x.TicketId)
            .GreaterThan(0).WithMessage("Ticket ID must be greater than zero.");

        RuleFor(x => x.VehicleId)
            .GreaterThan(0).WithMessage("Vehicle ID must be greater than zero.");
    }
}
