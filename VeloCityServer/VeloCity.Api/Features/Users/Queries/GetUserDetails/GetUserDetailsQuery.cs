using MediatR;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Users.Queries.GetUserDetails;

public record GetUserDetailsQuery(int UserId) : IRequest<UserDetailsDto>;

public record UserDetailsDto(
    int Id,
    string Email,
    string Name,
    string Surname,
    decimal Balance,
    UserRole Role,
    bool IsActive
);
