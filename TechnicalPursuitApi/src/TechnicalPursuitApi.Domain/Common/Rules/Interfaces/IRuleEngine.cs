using TechnicalPursuitApi.Domain.Common.Rules.Utils;

namespace TechnicalPursuitApi.Domain.Common.Rules.Interfaces;

public interface IRuleEngine<T>
{
     void AddRule(IRule<T> rule);
     List<RuleResult> CheckRules(T entity);
}