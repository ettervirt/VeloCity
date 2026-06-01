using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.DeleteLine;

public class DeleteLineHandler(ApplicationDbContext context) 
    : IRequestHandler<DeleteLineCommand>
{
    public async Task Handle(DeleteLineCommand request, CancellationToken ct)
    {
        var line = await context.Lines
            .FirstOrDefaultAsync(l => l.Id == request.Id && l.IsActive, ct);
        if (line is null) throw new NotFoundException("Line", request.Id);

        line.IsActive = false;

        await context.SaveChangesAsync(ct);
    }
}
