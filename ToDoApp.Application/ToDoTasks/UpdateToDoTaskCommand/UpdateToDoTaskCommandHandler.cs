using MassTransit.Mediator;
using ToDoApp.Application.Abstractions;
using ToDoApp.Domain.ToDoTasks;
using ToDoApp.SharedKernel;

namespace ToDoApp.Application.ToDoTasks.UpdateToDoTaskCommand;

public record UpdateToDoTaskCommand(Guid Id, string Title, string? Description, DateTime ExpirationDateTime)
    : Request<Result>;

public class UpdateToDoTaskCommandHandler(
    IToDoTaskRepository repository,
    IUnitOfWork unitOfWork
) : MediatorRequestHandler<UpdateToDoTaskCommand, Result>
{
    protected override async Task<Result> Handle(UpdateToDoTaskCommand request, CancellationToken cancellationToken)
    {
        var todoTask = await repository.GetAsync(request.Id, cancellationToken);
        if (todoTask is null) return ToDoTaskErrors.ToDoTaskNotFound;

        var result = todoTask.Update(request.Title, request.Description, request.ExpirationDateTime);
        if (result.IsFailure) return result.Error;

        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}