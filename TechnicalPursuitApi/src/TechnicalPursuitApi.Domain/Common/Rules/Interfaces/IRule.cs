using TechnicalPursuitApi.Domain.Common.Rules.Utils;

namespace TechnicalPursuitApi.Domain.Common.Rules.Interfaces;

public interface IRule<T>
{
    RuleResult Check(T entity);
    object SetRuleCode(string ruleCode);
}