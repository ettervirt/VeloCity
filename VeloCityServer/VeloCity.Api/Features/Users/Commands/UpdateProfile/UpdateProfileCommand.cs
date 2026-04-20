using FluentValidation;
using MediatR;
using VeloCity.Api.Features.Users.Queries.GetProfile;

namespace VeloCity.Api.Features.Users.Commands.UpdateProfile;

public record UpdateProfileCommand(
    string Name,
    string Surname) : IRequest<ProfileDto>;
