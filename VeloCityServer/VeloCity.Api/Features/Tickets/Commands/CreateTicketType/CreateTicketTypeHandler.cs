using MediatR;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Tickets.Commands.CreateTicketType;

public class CreateTicketTypeHandler(ApplicationDbContext context)
    : IRequestHandler<CreateTicketTypeCommand, int>
{
    public async Task<int> Handle(CreateTicketTypeCommand request, CancellationToken ct)
    {
        var ticketType = new TicketType
        {
            Name = request.Name,
            Price = request.Price,
            DurationInMinutes = request.DurationInMinutes,
            ZoneLimit = request.ZoneLimit,
            IsActive = true
        };

        context.TicketTypes.Add(ticketType);
        await context.SaveChangesAsync(ct);

        return ticketType.Id;
    }
}
