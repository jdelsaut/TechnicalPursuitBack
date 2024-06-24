using ErrorOr;

using MediatR;

using TechnicalPursuitApi.Application.Services;
using TechnicalPursuitApi.Domain.Common.Rules.Utils;
using TechnicalPursuitApi.Domain.Rules.Entity;

namespace TechnicalPursuitApi.Application.Rules.Queries.Evaluate;

public class EvaluateQueryHandler : IRequestHandler<EvaluateQuery, ErrorOr<RuleResult>>
{
    private readonly Dictionary<string, RuleDetails> _ruleHandlers;

    public EvaluateQueryHandler(RulesService ruleHandlers)
    {
        _ruleHandlers = ruleHandlers.Rules;
    }

    public Task<ErrorOr<RuleResult>> Handle(EvaluateQuery query, CancellationToken cancellationToken)
    {
        return null!;
    }
}