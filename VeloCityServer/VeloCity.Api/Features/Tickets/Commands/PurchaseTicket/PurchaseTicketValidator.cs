using FluentValidation;

namespace VeloCity.Api.Features.Tickets.Commands.PurchaseTicket;

public class PurchaseTicketValidator : AbstractValidator<PurchaseTicketCommand>
{
    public PurchaseTicketValidator()
    {
        RuleFor(x => x.TicketTypeId)
            .GreaterThan(0).WithMessage("Ticket Type ID must be greater than zero.");
    }
}
