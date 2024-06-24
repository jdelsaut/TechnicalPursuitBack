using FluentValidation;

namespace TechnicalPursuitApi.Application.TechnicalPursuitApi.Commands;

public class AddManyQuestionsCommandValidator : AbstractValidator<AddManyQuestionsCommand>
{
    public AddManyQuestionsCommandValidator()
    {
        RuleFor(command => command.Questions).NotNull();
    }
}