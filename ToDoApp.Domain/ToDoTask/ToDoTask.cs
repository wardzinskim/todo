using ToDoApp.Domain.ToDoTask.Events;
using ToDoApp.SharedKernel;

namespace ToDoApp.Domain.ToDoTask;

public class ToDoTask : Entity, IAggregateRoot, IAuditable
{
    public ToDoTaskId Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public byte PercentageCompletion { get; init; }

    public DateTime CreationDate { get; }
    public DateTime? LastUpdated { get; }


    private ToDoTask(ToDoTaskId id, string title, string? description)
    {
        Id = id;
        Title = title;
        Description = description;
        PercentageCompletion = 0;

        AddDomainEvent(new ToDoTaskCreatedEvent(Id));
    }


    public static Result<ToDoTask> Create(
        IIdGenerator idGenerator,
        string title,
        string? description
    )
    {
        var todoTaskId = ToDoTaskId.Of(idGenerator.NextId());
        if (todoTaskId.IsFailure) return todoTaskId.Error;

        return new ToDoTask(todoTaskId.Value, title, description);
    }
}