namespace TechnicalPursuitApi.Domain.Common.Models;

/// <summary>
/// Interface for entities that have domain events.
/// </summary>
public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    public void ClearDomainEvents();
}