using ErrorOr;

using TechnicalPursuitApi.Domain.Common.Models;

namespace TechnicalPursuitApi.Domain.ValueObjects
{
    public sealed class QuestionId : AggregateRootId<Guid>
    {
        private QuestionId(Guid value) : base(value)
        {
        }

        public static QuestionId Create(Guid value)
        {
            return new QuestionId(value);
        }

        public static QuestionId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static ErrorOr<QuestionId> Create(string value)
        {
            if (!Guid.TryParse(value, out var guid))
            {
                return Common.DomainErrors.Errors.Joueur.InvalidJoueurId;
            }

            return new QuestionId(guid);
        }
    }
}