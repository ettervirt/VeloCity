namespace VeloCity.Api.Common.Exceptions;

public class NotFoundException(string entityName, object entityId)
    : Exception($"{entityName} ID {entityId} was not found.")
{
    public string EntityName { get; } = entityName;
    public object EntityId { get; } = entityId;
}
