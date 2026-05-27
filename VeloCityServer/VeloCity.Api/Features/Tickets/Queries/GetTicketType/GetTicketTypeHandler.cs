using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Features.Tickets.Queries.GetActiveTicketTypes;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Tickets.Queries.GetTicketType;

public class GetTicketTypeHandler(ApplicationDbContext context)
    : IRequestHandler<GetTicketTypeQuery, TicketTypeDto>
{
    public async Task<TicketTypeDto> Handle(GetTicketTypeQuery request, CancellationToken ct)
    {
        var ticketType = await context.TicketTypes.FindAsync([request.Id], ct)
                         ?? throw new NotFoundException("TicketType", request.Id);

        return new TicketTypeDto(
            ticketType.Id,
            ticketType.Name,
            ticketType.Price,
            ticketType.DurationInMinutes,
            ticketType.ZoneLimit);
    }
}
