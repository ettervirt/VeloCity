using MediatR;
using Microsoft.EntityFrameworkCore;
using VeloCity.Api.Common.Exceptions;
using VeloCity.Api.Models;
using VeloCity.Api.Models.Data;
using VeloCity.Api.Models.Enums;

namespace VeloCity.Api.Features.Users.Commands.CreateUser;

public class CreateUserHandler(ApplicationDbContext context) : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand request, CancellationToken ct)
    {
        bool emailExist = await context.Users.AnyAsync(u => u.Email == request.Email, ct);
        if (emailExist)
            throw new AppException("Email already exists", 400);

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            PasswordHash = hashedPassword,
            Role = UserRole.Passenger,
            Balance = 0.00m,
            IsActive = true
        };

        context.Users.Add(user);
        await context.SaveChangesAsync(ct);
        return user.Id;
    }
}
