using MediatR;
using VeloCity.Api.Features.Lines.Commands.DTOs;

namespace VeloCity.Api.Features.Lines.Commands.UpdateLine;

public record UpdateLineCommand(int Id, string Name) : IRequest<LineDto>;

public record UpdateLineBody(string Name);
