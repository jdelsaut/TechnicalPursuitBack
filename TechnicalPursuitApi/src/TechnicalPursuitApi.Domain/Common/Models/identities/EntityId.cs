namespace TechnicalPursuitApi.Domain.Common.Models;

/// <summary>
/// Base class for entity identifiers.
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
public abstract class EntityId<TId> : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityId{TId}"/> class.
    /// </summary>
    /// <param name="value">The value of the identifier.</param>
    protected EntityId(TId value)
    {
        Value = value;
    }

#pragma warning disable CS8618
    protected EntityId()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Gets the value of the identifier.
    /// </summary>
    public TId Value { get; }

    /// <inheritdoc/>
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    /// <inheritdoc/>
    public override string? ToString() => Value?.ToString() ?? base.ToString();
}