using MediatR;
using VeloCity.Api.Features.Lines.Commands.DTOs;

namespace VeloCity.Api.Features.Lines.Queries.GetLineDetails;

public record GetLineDetailsQuery(int Id) : IRequest<LineDetailsDto?>;
