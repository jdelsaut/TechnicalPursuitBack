using MediatR;
using TechnicalPursuitApi.Domain;

namespace TechnicalPursuitApi.Application.TechnicalPursuitApi.Queries;

public record GetManyQuestionsQuery(IEnumerable<string> QuestionIds) : IRequest<IEnumerable<Question>>;