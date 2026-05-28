using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Features.Lines.Commands.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Queries.GetLineDetails;

public class GetLineDetailsHandler(ApplicationDbContext context) 
    : IRequestHandler<GetLineDetailsQuery, LineDetailsDto?>
{
   public async Task<LineDetailsDto?> Handle(GetLineDetailsQuery request, CancellationToken ct)
    {
        return await context.Lines
            .AsNoTracking()
            .Where(l => l.Id == request.Id && l.IsActive)
            .Select(l => new LineDetailsDto(
                l.Id,
                l.Name,
                l.RouteStops
                    .OrderBy(rs => rs.Direction)
                    .ThenBy(rs => rs.Sequence)
                    .Select(rs => new LineRouteStopDto(
                        rs.StopId,
                        rs.Stop.Name,
                        rs.Sequence,
                        rs.Direction))
                    .ToList()
            ))
            .FirstOrDefaultAsync(ct);
    }
}
