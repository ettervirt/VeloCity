using MediatR;
using VeloCity.Api.Common.Pagination;

namespace VeloCity.Api.Features.Users.Queries.GetUsers;

public class GetUsersQuery : PaginatedRequest, IRequest<PaginatedList<UserDto>>
{
    public string? Search { get; set; }
    public bool? IsActive { get; set; }
}

public record UserDto(
    int Id,
    string Email,
    string Name,
    string Surname,
    decimal Balance,
    bool IsActive);
