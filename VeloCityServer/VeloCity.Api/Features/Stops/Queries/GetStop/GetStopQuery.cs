using MediatR;
using VeloCity.Api.Features.Stops.DTOs;

namespace VeloCity.Api.Features.Stops.Queries.GetStop;

public record GetStopQuery(int Id) : IRequest<StopDto>;
