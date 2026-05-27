using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Tickets.Commands.UpdateTicketType;

public class UpdateTicketTypeHandler(ApplicationDbContext context)
    : IRequestHandler<UpdateTicketTypeCommand>
{
    public async Task Handle(UpdateTicketTypeCommand request, CancellationToken ct)
    {
        var ticketType = await context.TicketTypes.FindAsync([request.Id], ct)
                         ?? throw new NotFoundException("TicketType", request.Id);

        ticketType.Name = request.Name;
        ticketType.Price = request.Price;
        ticketType.DurationInMinutes = request.DurationInMinutes;
        ticketType.ZoneLimit = request.ZoneLimit;

        await context.SaveChangesAsync(ct);
    }
}
