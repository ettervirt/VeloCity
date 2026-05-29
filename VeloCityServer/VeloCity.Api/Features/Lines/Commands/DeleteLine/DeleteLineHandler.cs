using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.DeleteLine;

public class DeleteLineHandler(ApplicationDbContext context) 
    : IRequestHandler<DeleteLineCommand, bool>
{
    public async Task<bool> Handle(DeleteLineCommand request, CancellationToken ct)
    {
        var line = await context.Lines
            .FirstOrDefaultAsync(l => l.Id == request.Id && l.IsActive, ct);
        if (line is null) return false;

        line.IsActive = false;

        await context.SaveChangesAsync(ct);
        return true;
    }
}
