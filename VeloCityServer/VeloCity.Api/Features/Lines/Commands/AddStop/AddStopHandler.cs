using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.AddStop;

public class AddStopHandler(ApplicationDbContext context) 
    : IRequestHandler<AddStopCommand>
{
    public async Task Handle(AddStopCommand request, CancellationToken ct)
    {
        var lineExists = await context.Lines
            .AnyAsync(l => l.Id == request.LineId && l.IsActive, ct);

        if (!lineExists)
        {
            throw new NotFoundException("Line", request.LineId);
        }

        var stopExists = await context.Stops
            .AnyAsync(s => s.Id == request.StopId, ct);

        if (!stopExists)
        {
            throw new NotFoundException("Stop", request.StopId);
        }

        var lastSequence = await context.RouteStops
            .Where(rs => rs.LineId == request.LineId && rs.Direction == request.Direction)
            .Select(rs => (int?)rs.Sequence)
            .MaxAsync(ct) ?? 0;

        var routeStop = new RouteStop
        {
            LineId = request.LineId,
            StopId = request.StopId,
            Direction = request.Direction,
            Sequence = lastSequence + 1
        };

        context.RouteStops.Add(routeStop);
        await context.SaveChangesAsync(ct);
    }
}
