using MediatR;

using TechnicalPursuitApi.Domain;

namespace TechnicalPursuitApi.Application.TechnicalPursuitApi.Commands;

public record AddAQuestionCommand(Question Question) : IRequest<bool>;