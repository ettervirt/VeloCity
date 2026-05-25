using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleHandler(ApplicationDbContext context) : IRequestHandler<UpdateVehicleRequest, bool>
{
    public async Task<bool> Handle(UpdateVehicleRequest request, CancellationToken ct)
    {
        var vehicle = await context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == request.Id && v.IsActive, ct);

        if (vehicle is null) return false;

        var duplicateExists = await context.Vehicles
            .AnyAsync(v => v.SideNumber == request.Command.SideNumber && v.Id != request.Id, ct);

        if (duplicateExists)
        {
            throw new AppException("This side number is already in use.", 400);
        }

        vehicle.SideNumber = request.Command.SideNumber;
        vehicle.Model = request.Command.Model;

        await context.SaveChangesAsync(ct);
        return true;
    }
}
