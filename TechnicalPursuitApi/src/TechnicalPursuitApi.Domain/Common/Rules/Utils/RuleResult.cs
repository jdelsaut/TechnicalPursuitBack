namespace TechnicalPursuitApi.Domain.Common.Rules.Utils;

public class RuleResult
{
    public bool IsRulePassed { get; set; }
    public string? ErrorMessage { get; set; }
    public object? Value { get; set; }
    public List<RuleResult>? RulesNotPassed { get; set; }
    public string CodeRule { get; set; } = null!;
}