using MediatR;

using TechnicalPursuitApi.Application.Interfaces;

namespace TechnicalPursuitApi.Application.TechnicalPursuitApi.Commands;

public class AddManyQuestionsCommandHandler : IRequestHandler<AddManyQuestionsCommand, bool>
{
    private readonly IQuestionRepository _repository;

    public AddManyQuestionsCommandHandler(IQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AddManyQuestionsCommand request, CancellationToken cancellationToken)
    {
        await _repository.AddManyItemsAsync(request.Questions);
        return true;
    }
}