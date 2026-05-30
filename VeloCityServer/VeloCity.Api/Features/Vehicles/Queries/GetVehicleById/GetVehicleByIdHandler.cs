using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Features.Vehicles.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Vehicles.Queries.GetVehicleById;

public class GetVehicleByIdHandler(ApplicationDbContext context) : IRequestHandler<GetVehicleByIdQuery, VehicleDto>
{
    public async Task<VehicleDto> Handle(GetVehicleByIdQuery request, CancellationToken ct)
    {
        var vehicle = await context.Vehicles
                          .AsNoTracking()
                          .FirstOrDefaultAsync(v => v.Id == request.Id, ct)
                      ?? throw new NotFoundException("Vehicle", request.Id);

        return new VehicleDto(vehicle.Id, vehicle.SideNumber, vehicle.Model, vehicle.IsActive);
    }
}
