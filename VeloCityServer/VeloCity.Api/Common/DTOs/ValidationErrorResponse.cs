namespace VeloCity.Api.Common.DTOs;

public class ValidationErrorResponse
{
    public string Title { get; init; } = "Validation Error";
    public int Status { get; init; } = 400;
    public Dictionary<string, string[]> Errors { get; init; } = new();
}
