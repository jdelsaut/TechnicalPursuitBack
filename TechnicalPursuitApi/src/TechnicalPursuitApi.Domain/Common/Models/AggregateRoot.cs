namespace TechnicalPursuitApi.Domain.Common.Models;

/// <summary>
/// Base class for aggregate roots in the domain model.
/// </summary>
/// <typeparam name="TId">The type of the aggregate root's identifier.</typeparam>
/// <typeparam name="TIdType">The underlying type of the aggregate root's identifier.</typeparam>
public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
    where TId : AggregateRootId<TIdType>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId, TIdType}"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the aggregate root.</param>
    protected AggregateRoot(TId id)
    {
        Id = id;
    }

#pragma warning disable CS8618
    protected AggregateRoot()
    {
    }
#pragma warning restore CS8618

#pragma warning disable CA1061

    /// <summary>
    /// Gets or sets the aggregate root's identifier.
    /// </summary>
    public new AggregateRootId<TIdType> Id { get; protected set; }

#pragma warning restore CA1061
}