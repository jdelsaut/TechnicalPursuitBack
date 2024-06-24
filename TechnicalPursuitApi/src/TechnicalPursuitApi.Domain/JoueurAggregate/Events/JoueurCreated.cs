using TechnicalPursuitApi.Domain.Common.Models;

namespace TechnicalPursuitApi.Domain.JoueurAggregate.Events
{
    public record JoueurCreated(Joueur Joueur) : IDomainEvent;
}