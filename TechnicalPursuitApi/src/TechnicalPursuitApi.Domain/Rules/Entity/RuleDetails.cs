using TechnicalPursuitApi.Domain.Common.Rules.Interfaces;

namespace TechnicalPursuitApi.Domain.Rules.Entity;

public record RuleDetails(string Description, IRule<TechnicalPursuitApi> Rule);