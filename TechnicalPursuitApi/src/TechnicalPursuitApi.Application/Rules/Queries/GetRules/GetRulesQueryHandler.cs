using ErrorOr;

using MediatR;

using TechnicalPursuitApi.Application.Services;
using TechnicalPursuitApi.Domain.Common.DomainErrors;
using TechnicalPursuitApi.Domain.Rules.Entity;

namespace TechnicalPursuitApi.Application.Rules.Queries.GetRules;

public class GetRulesQueryHandler : IRequestHandler<GetRulesQuery, ErrorOr<Dictionary<string, RuleDetails>>>
{
    private readonly Dictionary<string, RuleDetails> _ruleHandlers;

    public GetRulesQueryHandler(RulesService ruleHandlers)
    {
        _ruleHandlers = ruleHandlers.Rules;
    }

    public Task<ErrorOr<Dictionary<string, RuleDetails>>> Handle(GetRulesQuery query, CancellationToken cancellationToken)
    {
        return Task.FromResult(_ruleHandlers switch
        {
            null => Errors.Rule.CollectionNotFound,
            _ => ErrorOrFactory.From(_ruleHandlers),
        });
    }
}