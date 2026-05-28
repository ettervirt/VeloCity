using MediatR;
using VeloCity.Api.Features.Lines.Commands.DTOs;

namespace VeloCity.Api.Features.Lines.Commands.CreateLine;

public record CreateLineCommand(string Name) : IRequest<LineDto>;
