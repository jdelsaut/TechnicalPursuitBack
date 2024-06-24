using FluentValidation;

namespace TechnicalPursuitApi.Application.TechnicalPursuitApi.Commands
{
    public class AddAQuestionCommandValidator : AbstractValidator<AddAQuestionCommand>
    {
        public AddAQuestionCommandValidator()
        {
            RuleFor(command => command.Question).NotNull();
        }
    }
}