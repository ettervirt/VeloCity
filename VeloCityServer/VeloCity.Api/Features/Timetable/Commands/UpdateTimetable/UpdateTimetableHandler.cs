using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Timetable.Commands.UpdateTimetable
{
    public class UpdateTimetableHandler(ApplicationDbContext context)
        :IRequestHandler<UpdateTimetableCommand>
    {
        public async Task Handle(UpdateTimetableCommand request, CancellationToken ct)
        {
            var timetable = await context.Timetables
                .FirstOrDefaultAsync(t => t.Id == request.Id, ct)
                ?? throw new NotFoundException(nameof(Timetable), request.Id);
            timetable.TripId = request.TripId;
            timetable.StopId = request.StopId;
            timetable.Sequence = request.Sequence;
            timetable.DepartureTime = request.DepartureTime;

            await context.SaveChangesAsync(ct);
        }
    }
}
