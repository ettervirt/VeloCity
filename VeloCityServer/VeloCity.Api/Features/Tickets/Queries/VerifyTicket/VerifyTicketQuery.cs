using MediatR;
using VeloCity.Api.Features.Tickets.DTOs;

namespace VeloCity.Api.Features.Tickets.Queries.VerifyTicket;

public record VerifyTicketQuery(int TicketId, int VehicleId) : IRequest<TicketVerificationResultDto>;
