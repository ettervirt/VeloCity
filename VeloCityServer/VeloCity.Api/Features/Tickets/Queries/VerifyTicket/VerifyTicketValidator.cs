using FluentValidation;

namespace VeloCity.Api.Features.Tickets.Queries.VerifyTicket;

public class VerifyTicketValidator : AbstractValidator<VerifyTicketQuery>
{
    public VerifyTicketValidator()
    {
        RuleFor(x => x.TicketId).GreaterThan(0).WithMessage("Invalid ticket ID.");
        RuleFor(x => x.VehicleId).GreaterThan(0).WithMessage("Invalid vehicle ID.");
    }
}
