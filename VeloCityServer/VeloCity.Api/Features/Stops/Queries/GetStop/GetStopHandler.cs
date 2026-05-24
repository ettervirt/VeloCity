using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Features.Stops.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Stops.Queries.GetStop;

public class GetStopHandler(ApplicationDbContext context) : IRequestHandler<GetStopQuery, StopDto>
{
    public async Task<StopDto> Handle(GetStopQuery request, CancellationToken ct)
    {
        var stop = await context.Stops.AsNoTracking()
                       .FirstOrDefaultAsync(s => s.Id == request.Id, ct)
                   ?? throw new NotFoundException("Stop", request.Id);

        return new StopDto(stop.Id, stop.Name, stop.Latitude, stop.Longitude, stop.ZoneId, stop.ExternalId,
            stop.IsActive);
    }
}
