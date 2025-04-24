using MassTransit.Mediator;
using ToDoApp.Application.Abstractions;
using ToDoApp.Domain.ToDoTasks;
using ToDoApp.SharedKernel;

namespace ToDoApp.Application.ToDoTasks.SetToDoTaskPercentageCompletionCommand;

public record SetToDoTaskPercentageCompletionCommand(Guid Id, int PercentageCompletion)
    : Request<Result>;

public class SetToDoTaskPercentageCompletionCommandHandler(
    IToDoTaskRepository repository,
    IUnitOfWork unitOfWork
) : MediatorRequestHandler<SetToDoTaskPercentageCompletionCommand, Result>
{
    protected override async Task<Result> Handle(
        SetToDoTaskPercentageCompletionCommand request,
        CancellationToken cancellationToken
    )
    {
        var todoTask = await repository.GetAsync(request.Id, cancellationToken);
        if (todoTask is null) return ToDoTaskErrors.ToDoTaskNotFound;

        var result = todoTask.SetPercentageCompletion(request.PercentageCompletion);
        if (result.IsFailure) return result.Error;

        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }
}