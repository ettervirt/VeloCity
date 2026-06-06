using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Timetable.Commands.DeleteTimetable;

public class DeleteTimetableHandler(ApplicationDbContext context)
    :IRequestHandler<DeleteTimetableCommand>
{
    public async Task Handle(DeleteTimetableCommand request, CancellationToken ct)
    {
        var timetable = await context.Timetables
            .FirstOrDefaultAsync(t => t.Id == request.Id, ct)
            ?? throw new NotFoundException(nameof(Timetable), request.Id);
        context.Timetables.Remove(timetable);
        await context.SaveChangesAsync(ct);
    }
}
