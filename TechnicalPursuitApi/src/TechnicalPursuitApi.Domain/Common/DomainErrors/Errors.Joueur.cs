using ErrorOr;

namespace TechnicalPursuitApi.Domain.Common.DomainErrors
{
    public static partial class Errors
    {
        public static class Joueur
        {
            public static Error InvalidJoueurId => Error.Validation(
                code: "Joueur.InvalidId",
                description: "Joueur ID is invalid");

            public static Error NotFound => Error.NotFound(
                code: "Joueur.NotFound",
                description: "Joueur with given ID does not exist");

            public static Error AlreadyExists => Error.Validation(
                code: "Joueur.AlreadyExists",
                description: "Joueur already exists");
        }
    }
}