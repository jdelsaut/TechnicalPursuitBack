using TechnicalPursuitApi.Domain.Common.Models;
using TechnicalPursuitApi.Domain.JoueurAggregate.Events;
using TechnicalPursuitApi.Domain.JoueurAggregate.ValueObjects;

namespace TechnicalPursuitApi.Domain.JoueurAggregate
{
    public sealed class Joueur : AggregateRoot<JoueurId, Guid>
    {
        private Joueur(
   JoueurId joueurId,
   uint score)
   : base(joueurId)
        {
            Score = score;
        }

#pragma warning disable CS8618
        private Joueur()
        {
        }
#pragma warning restore CS8618
        public uint Score { get; set; }

        public static Joueur Create(
           uint score)
        {
            var joueur = new Joueur(
                JoueurId.CreateUnique(),
                score);

            joueur.AddDomainEvent(new JoueurCreated(joueur));

            return joueur;
        }
    }
}