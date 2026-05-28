namespace VeloCity.Api.Features.Lines.Commands.DTOs;

public record LineDto(
    int Id, 
    string Name, 
    bool IsActive);
