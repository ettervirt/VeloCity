using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Vehicles.Commands.UpdateVehicle;

public class UpdateVehicleHandler(ApplicationDbContext context) : IRequestHandler<UpdateVehicleCommand>
{
    public async Task Handle(UpdateVehicleCommand request, CancellationToken ct)
    {
        var vehicle = await context.Vehicles.FindAsync([request.Id], ct)
                      ?? throw new NotFoundException("Vehicle", request.Id);

        if (!vehicle.IsActive)
        {
            throw new AppException("Vehicle is inactive and cannot be updated.", 400);
        }

        var duplicateExists = await context.Vehicles
            .AnyAsync(v => v.SideNumber == request.SideNumber && v.Id != request.Id, ct);

        if (duplicateExists)
        {
            throw new AppException("This side number is already in use.", 400);
        }

        vehicle.SideNumber = request.SideNumber;
        vehicle.Model = request.Model;

        await context.SaveChangesAsync(ct);
    }
}
