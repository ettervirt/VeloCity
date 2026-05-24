using MediatR;
using VeloCity.Api.Models.Data;
using VeloCity.Api.Models;

namespace VeloCity.Api.Features.Stops.Commands.CreateStop;

public class CreateStopHandler(ApplicationDbContext context) : IRequestHandler<CreateStopCommand, int>
{
    public async Task<int> Handle(CreateStopCommand request, CancellationToken ct)
    {
        var stop = new Stop
        {
            Name = request.Name,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            ZoneId = request.Zone,
            ExternalId = request.ExternalId,
            IsActive = true
        };

        context.Stops.Add(stop);
        await context.SaveChangesAsync(ct);

        return stop.Id;
    }
}
