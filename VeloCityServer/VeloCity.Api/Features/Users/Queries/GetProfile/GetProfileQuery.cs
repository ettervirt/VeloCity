using MediatR;

namespace VeloCity.Api.Features.Users.Queries.GetProfile;

public record GetProfileQuery() : IRequest<ProfileDto?>;

public record ProfileDto(
    string Name,
    string Surname,
    string Email,
    string Role);
