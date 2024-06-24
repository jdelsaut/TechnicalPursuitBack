using MediatR;

using TechnicalPursuitApi.Domain;

namespace TechnicalPursuitApi.Application.TechnicalPursuitApi.Commands;

public record AddManyQuestionsCommand(IEnumerable<Question> Questions) : IRequest<bool>;