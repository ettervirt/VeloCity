using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Stops.Commands.DeleteStop;

public class DeleteStopHandler(ApplicationDbContext context) : IRequestHandler<DeleteStopCommand>
{
    public async Task Handle(DeleteStopCommand request, CancellationToken ct)
    {
        var stop = await context.Stops.FindAsync([request.Id], ct)
                   ?? throw new AppException("Stop not found", 404);
        if (!stop.IsActive)
            throw new AppException("Stop is already inactive.", 400);
        stop.IsActive = false;
        await context.SaveChangesAsync(ct);
    }
}
