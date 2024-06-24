using MediatR;

namespace TechnicalPursuitApi.Domain.Common.Models;

public interface IDomainEvent : INotification
{
    // DateTime OccuredOn { get; }
}