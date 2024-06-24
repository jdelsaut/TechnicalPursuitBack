using TechnicalPursuitApi.Domain.Common.Rules.Interfaces;

namespace TechnicalPursuitApi.Domain.Common.Rules.Utils;

public class And : IRule<object>
{
    private List<IRule<object>> _rules = new();
    private string _ruleCode = null!;

    public And(IRule<object> r1, IRule<object> r2)
    {
        _rules.Add(r1);
        _rules.Add(r2);
    }

    public RuleResult Check(object entity)
    {
        List<RuleResult> rules = new();
        List<RuleResult> rulesFailed = new();

        foreach (var result in _rules.Select(rule => rule.Check(entity)))
        {
            if (!result.IsRulePassed)
            {
                rulesFailed.Add(new RuleResult() { IsRulePassed = false, Value = result.Value, CodeRule = _ruleCode });
                continue;
            }

            rules.Add(new RuleResult() { IsRulePassed = true, Value = result.Value, CodeRule = $"{_ruleCode}_{result.Value}" });
        }

        return rulesFailed.Count != 0 ?
            new RuleResult() { IsRulePassed = false, RulesNotPassed = rulesFailed, CodeRule = _ruleCode } :
            new RuleResult() { IsRulePassed = true, Value = true, RulesNotPassed = rules, CodeRule = _ruleCode };
    }

    public object SetRuleCode(string ruleCode)
    {
        _ruleCode = ruleCode;

        return this;
    }
}