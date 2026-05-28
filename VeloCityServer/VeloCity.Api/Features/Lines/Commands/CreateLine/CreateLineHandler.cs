using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Features.Lines.Commands.DTOs;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Commands.CreateLine;

public class CreateLineHandler(ApplicationDbContext context)
    : IRequestHandler<CreateLineCommand, LineDto>
{
    public async Task<LineDto> Handle(CreateLineCommand request, CancellationToken ct)
    {
        var exists = await context.Lines
            .AnyAsync(l => l.Name.ToLower() == request.Name.ToLower() && l.IsActive, ct);
        if (exists) throw new AppException("Line already exists.", 400);

        var line = new Line
        {
            Name = request.Name,
            IsActive = true
        };

        context.Lines.Add(line);
        await context.SaveChangesAsync(ct);

        return new LineDto(line.Id, line.Name, line.IsActive);
    }
}
