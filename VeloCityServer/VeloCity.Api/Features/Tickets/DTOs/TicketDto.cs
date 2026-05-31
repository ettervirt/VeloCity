namespace VeloCity.Api.Features.Tickets.DTOs;

public record TicketDto(
    int Id,
    string TicketTypeName,
    decimal Price,
    DateTime PurchasedAt,
    DateTime? ValidFrom,
    DateTime? ValidTo,
    int? VehicleId,
    bool IsValidated);
