using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.UpdateSequence
{
    public class UpdateSequenceHandler(ApplicationDbContext context) 
        : IRequestHandler<UpdateSequenceCommand, bool>
    {
        public async Task<bool> Handle(UpdateSequenceCommand request, CancellationToken ct)
        {
            var currentStops = await context.RouteStops
                .Where(rs => rs.LineId == request.LineId && rs.Direction == request.Direction)
                .ToListAsync(ct);

            for (int i = 0; i < request.NewStopIds.Count; i++)
            {
                var targetStopId = request.NewStopIds[i];
                var match = currentStops.FirstOrDefault(rs => rs.StopId == targetStopId);

                if (match is not null)
                {
                    match.Sequence = i + 1;
                }
            }

            await context.SaveChangesAsync(ct);
            return true;
        }
    }
}
