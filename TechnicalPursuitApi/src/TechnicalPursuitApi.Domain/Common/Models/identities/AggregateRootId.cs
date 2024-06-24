namespace TechnicalPursuitApi.Domain.Common.Models;

/// <summary>
/// Base class for aggregate root identifiers.
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
public abstract class AggregateRootId<TId> : EntityId<TId>
{
    protected AggregateRootId(TId value) : base(value)
    {
    }
}