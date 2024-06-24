using ErrorOr;

using MediatR;

using TechnicalPursuitApi.Domain.Rules.Entity;

namespace TechnicalPursuitApi.Application.Rules.Queries.GetRules;

public record GetRulesQuery() : IRequest<ErrorOr<Dictionary<string, RuleDetails>>>;