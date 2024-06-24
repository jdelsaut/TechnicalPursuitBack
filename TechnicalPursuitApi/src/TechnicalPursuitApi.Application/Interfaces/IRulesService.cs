using TechnicalPursuitApi.Domain.Rules.Entity;

namespace TechnicalPursuitApi.Application.Interfaces;

public interface IRulesService
{
    public Dictionary<string, RuleDetails> Rules { get; }
}