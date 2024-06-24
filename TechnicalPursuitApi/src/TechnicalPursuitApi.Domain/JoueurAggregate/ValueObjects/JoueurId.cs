using ErrorOr;

using TechnicalPursuitApi.Domain.Common.Models;

namespace TechnicalPursuitApi.Domain.JoueurAggregate.ValueObjects
{
    public sealed class JoueurId : AggregateRootId<Guid>
    {
        private JoueurId(Guid value) : base(value)
        {
        }

        public static JoueurId Create(Guid value)
        {
            return new JoueurId(value);
        }

        public static JoueurId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static ErrorOr<JoueurId> Create(string value)
        {
            if (!Guid.TryParse(value, out var guid))
            {
                return Common.DomainErrors.Errors.Joueur.InvalidJoueurId;
            }

            return new JoueurId(guid);
        }
    }
}