using MediatR;

namespace VeloCity.Api.Features.Lines.Commands.DeleteLine;

public record DeleteLineCommand(int Id) : IRequest;
