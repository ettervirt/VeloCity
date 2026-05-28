using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Features.Lines.Commands.DTOs;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Lines.Queries.GetLines;

public class GetLinesHandler(ApplicationDbContext context) : IRequestHandler<GetLinesQuery, PaginatedList<LineDto>>
{
    public async Task<PaginatedList<LineDto>> Handle(GetLinesQuery request, CancellationToken ct)
    {
        var query = context.Lines.AsNoTracking().AsQueryable();

        query = query.Where(l => l.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(l => l.Name.ToLower().Contains(request.SearchTerm.ToLower()));
        }

        query = request.IsDescending
            ? query.OrderByDescending(l => l.Name)
            : query.OrderBy(l => l.Name);

        var dtoQuery = query.Select(l => new LineDto(l.Id, l.Name, l.IsActive));

        return await PaginatedList<LineDto>.CreateAsync(dtoQuery, request.PageNumber, request.PageSize, ct);
    }
}
