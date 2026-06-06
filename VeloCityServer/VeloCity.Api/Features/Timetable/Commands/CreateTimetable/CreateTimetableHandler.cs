using MediatR;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Timetable.Commands.CreateTimetable
{
    public class CreateTimetableHandler(ApplicationDbContext context)
        : IRequestHandler<CreateTimetableCommand, int>
    {
        public async Task<int> Handle(CreateTimetableCommand request, CancellationToken ct)
        {
            var timetable = new VeloCity.Api.Models.Timetable
            {
                TripId = request.TripId,
                StopId = request.StopId,
                Sequence = request.Sequence,
                DepartureTime = request.DepartureTime
            };
            context.Timetables.Add(timetable);
            await context.SaveChangesAsync(ct);
            return timetable.Id;
        }
    }
}
