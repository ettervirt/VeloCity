using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Stops.Commands.UpdateStop;

public class UpdateStopHandler(
    ApplicationDbContext context) : IRequestHandler<UpdateStopCommand>
{
    public async Task Handle(UpdateStopCommand request,
        CancellationToken ct)
    {
        var stop = await context.Stops.FindAsync([request.Id], ct)
                   ?? throw new NotFoundException("Stop", request.Id);

        stop.Name = request.Name;
        stop.Latitude = request.Latitude;
        stop.Longitude = request.Longitude;
        stop.ZoneId = request.Zone;
        stop.ExternalId = request.ExternalId;

        await context.SaveChangesAsync(ct);
    }
}
