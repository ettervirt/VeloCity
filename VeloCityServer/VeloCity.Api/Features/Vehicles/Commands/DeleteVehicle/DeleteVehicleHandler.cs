using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Vehicles.Commands.DeleteVehicle;

public class DeleteVehicleHandler(ApplicationDbContext context) : IRequestHandler<DeleteVehicleCommand>
{
    public async Task Handle(DeleteVehicleCommand request, CancellationToken ct)
    {
        var vehicle = await context.Vehicles.FindAsync([request.Id], ct)
                      ?? throw new NotFoundException("Vehicle", request.Id);

        if (!vehicle.IsActive)
            throw new AppException("Vehicle already inactive.", 400);

        vehicle.IsActive = false;
        await context.SaveChangesAsync(ct);
    }
}
