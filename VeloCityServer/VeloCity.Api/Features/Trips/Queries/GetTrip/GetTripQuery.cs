using MediatR;
using VeloCity.Api.Features.Trips.DTOs;

namespace VeloCity.Api.Features.Trips.Queries.GetTrip;

public record GetTripQuery(int Id) : IRequest<TripDto>;
