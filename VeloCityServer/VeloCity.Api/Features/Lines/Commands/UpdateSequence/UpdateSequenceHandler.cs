using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.UpdateSequence;

public class UpdateSequenceHandler(ApplicationDbContext context) 
    : IRequestHandler<UpdateSequenceCommand, bool>
{
    public async Task<bool> Handle(UpdateSequenceCommand request, CancellationToken ct)
    {
        var lineExists = await context.Lines.AnyAsync(l => l.Id == request.LineId && l.IsActive, ct);
        if (!lineExists) throw new AppException("Line not found.", 404);

        var currentStops = await context.RouteStops
            .Where(rs => rs.LineId == request.LineId && rs.Direction == request.Direction)
            .ToListAsync(ct);

        context.RouteStops.RemoveRange(currentStops);

        for (int i = 0; i < request.NewStopIds.Count; i++)
        {
            var newRouteStop = new RouteStop
            {
                LineId = request.LineId,
                StopId = request.NewStopIds[i],
                Direction = request.Direction,
                Sequence = i + 1
            };

            context.RouteStops.Add(newRouteStop);
        }

        await context.SaveChangesAsync(ct);
        return true;
    }
}
