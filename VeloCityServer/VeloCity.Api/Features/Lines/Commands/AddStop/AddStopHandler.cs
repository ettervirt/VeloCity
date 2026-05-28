using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.AddStop;

public class AddStopHandler(ApplicationDbContext context) 
    : IRequestHandler<AddStopCommand, bool>
{
    public async Task<bool> Handle(AddStopCommand request, CancellationToken ct)
    {
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
        return true;
    }
}
