using MassTransit.Mediator;
using ToDoApp.Application.Abstractions;
using ToDoApp.Domain.ToDoTasks;
using ToDoApp.SharedKernel;

namespace ToDoApp.Application.ToDoTasks.CreateToDoTaskCommand;

public record CreateToDoTaskCommand(string Title, string? Description, DateTime ExpirationDateTime)
    : Request<Result<Guid>>;

public class CreateToDoTaskCommandHandler(
    IIdGenerator idGenerator,
    IToDoTaskRepository repository,
    IUnitOfWork unitOfWork
)
    : MediatorRequestHandler<CreateToDoTaskCommand, Result<Guid>>
{
    protected override async Task<Result<Guid>> Handle(
        CreateToDoTaskCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = ToDoTask.Create(idGenerator, request.Title, request.Description, request.ExpirationDateTime);
        if (result.IsFailure) return result.Error;

        await repository.AddAsync(result.Value, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(result.Value.Id.Value);
    }
}