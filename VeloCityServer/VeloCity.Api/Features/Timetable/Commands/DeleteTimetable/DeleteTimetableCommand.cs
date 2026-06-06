using MediatR;

namespace VeloCity.Api.Features.Timetable.Commands.DeleteTimetable;

public record DeleteTimetableCommand(int Id) : IRequest;
