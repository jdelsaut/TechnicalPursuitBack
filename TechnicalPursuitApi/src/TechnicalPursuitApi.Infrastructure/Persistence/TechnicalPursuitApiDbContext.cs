using System.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using TechnicalPursuitApi.Application.Common.Interfaces.Persistence;
using TechnicalPursuitApi.Domain;
using TechnicalPursuitApi.Domain.Common.Models;
using TechnicalPursuitApi.Domain.JoueurAggregate;
using TechnicalPursuitApi.Infrastructure.Persistence.Interceptors;

namespace TechnicalPursuitApi.Infrastructure;

/// <summary>
/// Represents the database context for the TechnicalPursuitApi application.
/// </summary>
public class TechnicalPursuitApiDbContext : DbContext, IUnitOfWork
{
    private readonly PublishDomainEventInterceptor _publishDomainEventInterceptor;

    private IDbContextTransaction _currentTransaction = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="TechnicalPursuitApiDbContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    /// <param name="publishDomainEventInterceptor">The interceptor for publishing domain events.</param>
    public TechnicalPursuitApiDbContext(
        DbContextOptions<TechnicalPursuitApiDbContext> options,
        PublishDomainEventInterceptor publishDomainEventInterceptor)
        : base(options)
    {
        _publishDomainEventInterceptor = publishDomainEventInterceptor;
    }

    /// <summary>
    /// Gets a value indicating whether this instance has an active transaction.
    /// </summary>
    public bool HasActiveTransaction => _currentTransaction != null;

    public DbSet<Question> Questions { get; set; }
    public DbSet<Joueur> Joueurs { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Gets the current transaction.
    /// </summary>
    /// <returns>The current transaction.</returns>
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    /// <summary>
    /// Begins a new transaction asynchronously.
    /// </summary>
    /// <returns>The new transaction.</returns>
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            return null!;
        }

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    /// <summary>
    /// Commits the specified transaction asynchronously.
    /// </summary>
    /// <param name="transaction">The transaction to commit.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }

        if (transaction != _currentTransaction)
        {
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
        }

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null!;
            }
        }
    }

    /// <summary>
    /// Rolls back the current transaction.
    /// </summary>
    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null!;
            }
        }
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return Database.CreateExecutionStrategy();
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*         modelBuilder.Model.GetEntityTypes()
                    .SelectMany(e => e.GetProperties())
                    .ToList()
                    .ForEach(p => p.ValueGenerated = ValueGenerated.Never); */

        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(TechnicalPursuitApiDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Configures the context with the given options. This method is called for each instance of the context that is created.
    /// </summary>
    /// <param name="optionsBuilder">A builder used to create or modify options for this context.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}