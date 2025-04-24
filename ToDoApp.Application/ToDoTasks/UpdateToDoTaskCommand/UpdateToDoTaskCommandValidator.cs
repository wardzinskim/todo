using FluentValidation;

namespace ToDoApp.Application.ToDoTasks.UpdateToDoTaskCommand;

public class UpdateToDoTaskCommandValidator : AbstractValidator<UpdateToDoTaskCommand>
{
    public UpdateToDoTaskCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(256);

        RuleFor(x => x.ExpirationDateTime)
            .NotEmpty();
    }
}