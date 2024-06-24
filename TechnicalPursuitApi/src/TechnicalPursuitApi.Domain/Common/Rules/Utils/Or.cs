using TechnicalPursuitApi.Domain.Common.Rules.Interfaces;

namespace TechnicalPursuitApi.Domain.Common.Rules.Utils;

public class Or : IRule<object>
{
    private List<IRule<object>> _rules = new();
    private string _ruleCode = null!;

    public Or(IRule<object> r1, IRule<object> r2)
    {
        _rules.Add(r1);
        _rules.Add(r2);
    }

    public RuleResult Check(object entity)
    {
        List<RuleResult> rulesFailed = new();

        foreach (var rule in _rules)
        {
            RuleResult result = rule.Check(entity);

            if (result.IsRulePassed)
            {
                return new RuleResult() { IsRulePassed = true, Value = result.Value, CodeRule = $"{_ruleCode}{result.CodeRule}" };
            }

            rulesFailed.Add(new RuleResult() { IsRulePassed = false, Value = result.Value, CodeRule = $"{_ruleCode}{result.CodeRule}" });
        }

        return new RuleResult() { IsRulePassed = false, Value = false, RulesNotPassed = rulesFailed, CodeRule = _ruleCode };
    }

    public object SetRuleCode(string ruleCode)
    {
        _ruleCode = ruleCode;

        return this;
    }
}