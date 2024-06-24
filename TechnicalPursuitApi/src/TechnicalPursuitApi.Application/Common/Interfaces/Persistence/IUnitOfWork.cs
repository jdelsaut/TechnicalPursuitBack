using Microsoft.EntityFrameworkCore.Storage;

namespace TechnicalPursuitApi.Application.Common.Interfaces.Persistence;

/// <summary>
/// Represents a unit of work for managing database transactions.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    bool HasActiveTransaction { get; }

    /// <summary>
    /// Commits all changes made in this unit of work to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);

    IDbContextTransaction GetCurrentTransaction();

    Task<IDbContextTransaction> BeginTransactionAsync();

    Task CommitTransactionAsync(IDbContextTransaction transaction);

    void RollbackTransaction();
    IExecutionStrategy CreateExecutionStrategy();
}