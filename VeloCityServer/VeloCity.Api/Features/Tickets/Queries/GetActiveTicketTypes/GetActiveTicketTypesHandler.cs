using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Tickets.Queries.GetActiveTicketTypes;

public class GetActiveTicketTypesHandler(ApplicationDbContext context):
    IRequestHandler<GetActiveTicketTypesQuery, List<TicketTypeDto>>
{
    public async Task<List<TicketTypeDto>> Handle(GetActiveTicketTypesQuery request, CancellationToken ct)
    {
        return await context.TicketTypes.AsNoTracking()
            .Where(t => t.IsActive)
            .OrderBy(t => t.Price)
            .Select(t => new TicketTypeDto(t.Id,
                t.Name,
                t.Price,
                t.DurationInMinutes,
                t.ZoneLimit))
            .ToListAsync(ct);
    }
}
