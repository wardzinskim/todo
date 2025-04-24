using FluentValidation;

namespace ToDoApp.Application.ToDoTasks.CreateToDoTaskCommand;

public class CreateToDoTaskCommandValidator : AbstractValidator<CreateToDoTaskCommand>
{
    public CreateToDoTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(256);

        RuleFor(x => x.ExpirationDateTime)
            .NotEmpty();
    }
}