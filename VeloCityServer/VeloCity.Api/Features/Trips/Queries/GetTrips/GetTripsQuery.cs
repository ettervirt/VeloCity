using MediatR;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Features.Trips.DTOs;

namespace VeloCity.Api.Features.Trips.Queries.GetTrips;

public record GetTripsQuery(
    string? SearchTerm,
    int PageNumber,
    int PageSize
    ) : IRequest<PaginatedList<TripDto>>;
