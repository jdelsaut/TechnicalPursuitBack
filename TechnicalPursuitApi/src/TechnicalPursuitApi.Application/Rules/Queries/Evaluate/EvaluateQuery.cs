using ErrorOr;

using MediatR;

using TechnicalPursuitApi.Domain.Common.Rules.Utils;

namespace TechnicalPursuitApi.Application.Rules.Queries.Evaluate;

public record EvaluateQuery(Guid TechnicalPursuitApiId, string Code) : IRequest<ErrorOr<RuleResult>>;