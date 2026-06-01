using MediatR;
using VeloCity.Api.Features.Lines.DTOs;

namespace VeloCity.Api.Features.Lines.Commands.UpdateLine;

public record UpdateLineCommand(int Id, string Name) : IRequest;

public record UpdateLineBody(string Name);
