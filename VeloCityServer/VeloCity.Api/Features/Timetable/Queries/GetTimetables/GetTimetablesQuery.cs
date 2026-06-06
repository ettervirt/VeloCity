using MediatR;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Features.Timetable.DTOs;

namespace VeloCity.Api.Features.Timetable.Queries.GetTimetables;

public record GetTimetablesQuery(
    string? SearchTerm,
    int PageNumber,
    int PageSize
    ) : IRequest<PaginatedList<TimetableDto>>;

