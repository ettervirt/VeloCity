using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Vehicles.Commands.DeleteVehicle;

public class DeleteVehicleHandler(ApplicationDbContext context) : IRequestHandler<DeleteVehicleCommand, bool>
{
    public async Task<bool> Handle(DeleteVehicleCommand request, CancellationToken ct)
    {
        var vehicle = await context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == request.Id, ct);

        if (vehicle == null) return false;
        if(vehicle.IsActive == false) throw new AppException("Vehicle not found", 400);
        // Soft delete
        vehicle.IsActive = false;

        await context.SaveChangesAsync(ct);
        return true;
    }
}
