using MediatR;
using VeloCity.Api.Features.Timetable.DTOs;

namespace VeloCity.Api.Features.Timetable.Queries.GetTimetable;

public record GetTimetableQuery(int Id) : IRequest<TimetableDto>;
