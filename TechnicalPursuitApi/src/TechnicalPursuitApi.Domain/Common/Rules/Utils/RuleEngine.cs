using TechnicalPursuitApi.Domain.Common.Rules.Interfaces;

namespace TechnicalPursuitApi.Domain.Common.Rules.Utils;

public class RuleEngine<T> : IRuleEngine<T>
{
    private readonly List<IRule<T>> _rules = new();

    public void AddRule(IRule<T> rule)
    {
        _rules.Add(rule);
    }

    public List<RuleResult> CheckRules(T entity)
    {
        return _rules.Select(rule => rule.Check(entity)).ToList();
    }
}