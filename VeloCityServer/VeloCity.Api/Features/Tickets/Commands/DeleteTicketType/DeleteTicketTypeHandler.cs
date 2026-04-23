using MediatR;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Tickets.Commands.DeleteTicketType
{
    public class DeleteTicketTypeHandler(ApplicationDbContext context)
    : IRequestHandler<DeleteTicketTypeCommand>
    {
        public async Task Handle(DeleteTicketTypeCommand request, CancellationToken ct)
        {
            var ticket = await context.TicketTypes.FindAsync([request.Id], ct)
                       ?? throw new NotFoundException("TicketType", request.Id);

            if (!ticket.IsActive)
            {
                throw new AppException("Ticket type already inactive", 400);
            }

            ticket.IsActive = false;

            await context.SaveChangesAsync(ct);
        }
    }
}
