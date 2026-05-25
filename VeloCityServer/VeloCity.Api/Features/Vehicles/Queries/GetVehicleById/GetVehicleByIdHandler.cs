using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Features.Vehicles.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Vehicles.Queries.GetVehicleById;

public class GetVehicleByIdHandler(ApplicationDbContext context) : IRequestHandler<GetVehicleByIdQuery, VehicleDto?>
{
    public async Task<VehicleDto?> Handle(GetVehicleByIdQuery request, CancellationToken ct)
    {
        var vehicle = await context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == request.Id, ct);

        if (vehicle is null) return null;

        return new VehicleDto(vehicle.Id, vehicle.SideNumber, vehicle.Model, vehicle.IsActive);
    }
}
