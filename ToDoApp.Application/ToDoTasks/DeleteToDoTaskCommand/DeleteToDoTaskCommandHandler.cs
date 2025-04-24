using MassTransit.Mediator;
using ToDoApp.Application.Abstractions;
using ToDoApp.Domain.ToDoTasks;
using ToDoApp.SharedKernel;

namespace ToDoApp.Application.ToDoTasks.DeleteToDoTaskCommand;

public record DeleteToDoTaskCommand(Guid Id) : Request<Result>;

public class DeleteToDoTaskCommandHandler(
    IToDoTaskRepository repository,
    IUnitOfWork unitOfWork
) : MediatorRequestHandler<DeleteToDoTaskCommand, Result>
{
    protected override async Task<Result> Handle(DeleteToDoTaskCommand request, CancellationToken cancellationToken)
    {
        var todoTask = await repository.GetAsync(request.Id, cancellationToken);
        if (todoTask is null) return ToDoTaskErrors.ToDoTaskNotFound;

        repository.Delete(todoTask);
        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }
}