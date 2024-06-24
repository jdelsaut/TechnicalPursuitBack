using MediatR;
using TechnicalPursuitApi.Domain;

namespace TechnicalPursuitApi.Application.TechnicalPursuitApi.Queries;

public record GetAQuestionQuery(string QuestionId) : IRequest<Question>;