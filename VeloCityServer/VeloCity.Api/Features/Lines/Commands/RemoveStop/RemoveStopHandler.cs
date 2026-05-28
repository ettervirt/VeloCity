using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.RemoveStop;

public class RemoveStopHandler(ApplicationDbContext context) 
    : IRequestHandler<RemoveStopCommand, bool>
{
    public async Task<bool> Handle(RemoveStopCommand request, CancellationToken ct)
    {
        var stopToRemove = await context.RouteStops
            .FirstOrDefaultAsync(rs => rs.LineId == request.LineId
                                && rs.StopId == request.StopId
                                && rs.Direction == request.Direction, ct);

        if (stopToRemove is null) return false;

        var remainingStops = await context.RouteStops
        .Where(rs => rs.LineId == request.LineId
                  && rs.Direction == request.Direction
                  && rs.StopId != request.StopId)
        .OrderBy(rs => rs.Sequence)
        .ToListAsync(ct);

        context.RouteStops.Remove(stopToRemove);

        //changing the sequence of the remaining stops
        for (int i = 0; i < remainingStops.Count; i++)
        {
            remainingStops[i].Sequence = i + 1;
        }

        await context.SaveChangesAsync(ct);
        return true;
    }
}
