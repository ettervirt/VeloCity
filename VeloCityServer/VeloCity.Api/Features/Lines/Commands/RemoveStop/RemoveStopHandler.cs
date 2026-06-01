using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.RemoveStop;

public class RemoveStopHandler(ApplicationDbContext context) 
    : IRequestHandler<RemoveStopCommand>
{
    public async Task Handle(RemoveStopCommand request, CancellationToken ct)
    {
        var stopToRemove = await context.RouteStops
            .FirstOrDefaultAsync(rs => rs.Id == request.RouteStopId, ct);

        if (stopToRemove is null) throw new NotFoundException("RouteStop", request.RouteStopId);

        var stopsToResequence = await context.RouteStops
        .Where(rs => rs.LineId == stopToRemove.LineId
                  && rs.Direction == stopToRemove.Direction
                  && rs.Sequence > stopToRemove.Sequence)
        .ToListAsync(ct);

        context.RouteStops.Remove(stopToRemove);

        //changing the sequence of the remaining stops
        foreach (var stop in stopsToResequence)
        {
            stop.Sequence -= 1;
        }

        await context.SaveChangesAsync(ct);
    }
}
