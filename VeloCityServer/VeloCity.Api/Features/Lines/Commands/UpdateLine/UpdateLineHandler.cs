using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Features.Lines.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.UpdateLine;

public class UpdateLineHandler(ApplicationDbContext context) 
    : IRequestHandler<UpdateLineCommand>
{
    public async Task Handle(UpdateLineCommand request, CancellationToken ct)
    {
        var line = await context.Lines
            .FirstOrDefaultAsync(l => l.Id == request.Id && l.IsActive, ct);

        if (line is null) throw new NotFoundException("Line", request.Id);

        var duplicateExists = await context.Lines
            .AnyAsync(l => EF.Functions.Like(l.Name, request.Name)
                        && l.Id != request.Id
                        && l.IsActive, ct);

        if (duplicateExists)
        {
            throw new AppException("Line with the same name already exists.", 400);
        }

        line.Name = request.Name;

        await context.SaveChangesAsync(ct);
    }
}
