using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Pagination;
using VeloCity.Api.Models.Data;

namespace VeloCity.Api.Features.Users.Queries.GetUsers;

public class GetUsersHandler(ApplicationDbContext context)
    : IRequestHandler<GetUsersQuery, PaginatedList<UserDto>>
{
    public async Task<PaginatedList<UserDto>> Handle(GetUsersQuery request, CancellationToken ct)
    {
        var query = context.Users.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(u =>
                u.Email.ToLower().Contains(search) ||
                u.Surname.ToLower().Contains(search) ||
                u.Name.ToLower().Contains(search));
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(u => u.IsActive == request.IsActive.Value);
        }

        query = request.IsDescending
            ? query.OrderByDescending(u => u.Surname)
            : query.OrderBy(u => u.Surname);

        var dtoQuery = query.Select(u => new UserDto(
            u.Id, u.Email, u.Name, u.Surname, u.Balance, u.IsActive));

        return await PaginatedList<UserDto>.CreateAsync(
            dtoQuery,
            request.PageNumber,
            request.PageSize,
            ct);
    }
}
