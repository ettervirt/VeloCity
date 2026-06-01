using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.UpdateSequence;

public class UpdateSequenceHandler(ApplicationDbContext context) 
    : IRequestHandler<UpdateSequenceCommand>
{
    public async Task Handle(UpdateSequenceCommand request, CancellationToken ct)
    {
        var lineExists = await context.Lines.AnyAsync(l => l.Id == request.LineId && l.IsActive, ct);
        if (!lineExists) throw new NotFoundException("Line", request.LineId);

        var currentStops = await context.RouteStops
            .Where(rs => rs.LineId == request.LineId && rs.Direction == request.Direction)
            .OrderBy(rs => rs.Sequence)
            .ToListAsync(ct);

        if(currentStops.Count != request.NewStopIds.Count)
            throw new ArgumentException("The number of new stop IDs must match the current number of stops in the line.");

        for (int i = 0; i < currentStops.Count; i++)
        {
            currentStops[i].StopId = request.NewStopIds[i];
            currentStops[i].Sequence = i + 1;
        }

        await context.SaveChangesAsync(ct);
    }
}
