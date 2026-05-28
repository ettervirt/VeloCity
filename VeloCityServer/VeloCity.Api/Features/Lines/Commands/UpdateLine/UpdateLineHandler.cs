using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Features.Lines.Commands.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.UpdateLine;

public class UpdateLineHandler(ApplicationDbContext context) 
    : IRequestHandler<UpdateLineCommand, LineDto>
{
    public async Task<LineDto> Handle(UpdateLineCommand request, CancellationToken ct)
    {
        var line = await context.Lines
            .FirstOrDefaultAsync(l => l.Id == request.Id && l.IsActive, ct);

        if (line is null) throw new AppException("Line not found.", 404);

        var duplicateExists = await context.Lines
            .AnyAsync(l => l.Name.ToLower() == request.Name.ToLower()
                        && l.Id != request.Id
                        && l.IsActive, ct);

        if (duplicateExists)
        {
            throw new AppException("Line with the same name already exists.", 400);
        }

        line.Name = request.Name;

        await context.SaveChangesAsync(ct);
        return new LineDto(line.Id, line.Name, line.IsActive);
    }
}
