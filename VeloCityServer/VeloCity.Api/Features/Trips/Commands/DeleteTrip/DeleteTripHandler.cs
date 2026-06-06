using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Trips.Commands.DeleteTrip;

public class DeleteTripHandler(ApplicationDbContext context)
    : IRequestHandler<DeleteTripCommand>
{
    public async Task Handle(DeleteTripCommand request, CancellationToken ct)
    {
        var trip = await context.Trips
            .FirstOrDefaultAsync(t => t.Id == request.Id, ct)
            ?? throw new NotFoundException("Trip", request.Id);
        trip.IsActive = false;
        await context.SaveChangesAsync(ct);
    }
}
