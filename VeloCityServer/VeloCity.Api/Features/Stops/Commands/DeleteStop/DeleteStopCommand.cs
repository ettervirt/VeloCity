using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Stops.Commands.DeleteStop;

public record DeleteStopCommand(int Id) : IRequest;
