using MediatR;

using TechnicalPursuitApi.Application.Interfaces;

namespace TechnicalPursuitApi.Application.TechnicalPursuitApi.Commands
{
    public class AddAQuestionCommandHandler : IRequestHandler<AddAQuestionCommand, bool>
    {
        private readonly IQuestionRepository _repository;

        public AddAQuestionCommandHandler(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AddAQuestionCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddItemAsync(request.Question);
            return true;
        }
    }
}