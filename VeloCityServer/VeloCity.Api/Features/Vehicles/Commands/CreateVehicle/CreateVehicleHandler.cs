using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Features.Vehicles.Queries.GetVehicle;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleHandler(ApplicationDbContext context)
        : IRequestHandler<CreateVehicleCommand, VehicleDto>
    {
        public async Task<VehicleDto> Handle(CreateVehicleCommand request, CancellationToken ct)
        {
            var duplicateExists = await context.Vehicles
                .AnyAsync(v => v.SideNumber == request.SideNumber, ct);
            if (duplicateExists)
            {
                throw new AppException("This side number is already in use.", 400);
            }
            var vehicle = new Vehicle
            {
                SideNumber = request.SideNumber,
                Model = request.Model,
                IsActive = true
            };
            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync(ct);
            return new VehicleDto(vehicle.Id, vehicle.SideNumber, vehicle.Model, vehicle.IsActive);
        }
    }
}
