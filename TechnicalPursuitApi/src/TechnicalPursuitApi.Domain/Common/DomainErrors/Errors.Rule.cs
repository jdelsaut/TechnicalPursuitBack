using ErrorOr;

namespace TechnicalPursuitApi.Domain.Common.DomainErrors;

public static partial class Errors
{
    public static class Rule
    {
        public static Error NotFound => Error.NotFound(
            code: "Rule.NotFound",
            description: "Rule with given code does not exist");

        public static Error CollectionNotFound => Error.NotFound(
            code: "Rule.CollectionNotFound",
            description: "Rule collection not found");
    }
}