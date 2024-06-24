using MediatR;

using TechnicalPursuitApi.Application.Interfaces;
using TechnicalPursuitApi.Domain;

namespace TechnicalPursuitApi.Application.TechnicalPursuitApi.Queries;

public class GetManyQuestionsQueryHandler : IRequestHandler<GetManyQuestionsQuery, IEnumerable<Question>>
{
    private readonly IQuestionRepository _repository;

    public GetManyQuestionsQueryHandler(IQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Question>> Handle(GetManyQuestionsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetManyItems(request.QuestionIds.First());
    }
}