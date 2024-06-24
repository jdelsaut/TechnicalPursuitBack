using MediatR;

using TechnicalPursuitApi.Application.Interfaces;
using TechnicalPursuitApi.Domain;

namespace TechnicalPursuitApi.Application.TechnicalPursuitApi.Queries;

public class GetAQuestionQueryHandler : IRequestHandler<GetAQuestionQuery, Question?>
{
    private readonly IQuestionRepository _repository;

    public GetAQuestionQueryHandler(IQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Question?> Handle(GetAQuestionQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetItem(request.QuestionId);
    }
}