namespace VeloCity.Api.Features.Tickets.DTOs;

public record TicketVerificationResultDto(
    bool IsValid,
    string Message,
    string? TicketTypeName,
    DateTime? ValidTo);
