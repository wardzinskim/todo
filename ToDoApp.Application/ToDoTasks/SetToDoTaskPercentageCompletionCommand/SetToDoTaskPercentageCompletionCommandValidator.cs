using FluentValidation;

namespace ToDoApp.Application.ToDoTasks.SetToDoTaskPercentageCompletionCommand;

public class SetToDoTaskPercentageCompletionCommandValidator : AbstractValidator<SetToDoTaskPercentageCompletionCommand>
{
    public SetToDoTaskPercentageCompletionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.PercentageCompletion)
            .InclusiveBetween(0, 100);
    }
}