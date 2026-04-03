using MediatR;
using Microsoft.EntityFrameworkCore;

namespace VeloCity.Api.Features.Users.Commands.GetProfile;

public record GetProfileQuery(
    int UserId) : IRequest<ProfileDto?>;

public record ProfileDto(
    string Name,
    string Surname,
    string Email,
    string Role);
