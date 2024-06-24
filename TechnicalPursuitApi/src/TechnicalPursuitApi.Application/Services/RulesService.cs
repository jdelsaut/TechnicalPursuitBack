using TechnicalPursuitApi.Application.Interfaces;
using TechnicalPursuitApi.Domain.Rules.Entity;

namespace TechnicalPursuitApi.Application.Services;

public class RulesService : IRulesService
{
    public Dictionary<string, RuleDetails> Rules => new(StringComparer.OrdinalIgnoreCase)
    {
        // { "RULE_NAME", YOUR_RULE) },
    };
}