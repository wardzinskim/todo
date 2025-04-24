using ToDoApp.Domain.ToDoTasks.Events;
using ToDoApp.Domain.ToDoTasks.Rules;
using ToDoApp.SharedKernel;

namespace ToDoApp.Domain.ToDoTasks;

public class ToDoTask : Entity, IAggregateRoot
{
    public ToDoTaskId Id { get; init; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public int PercentageCompletion { get; private set; }
    public DateTime ExpirationDateTime { get; private set; }


    private ToDoTask(ToDoTaskId id, string title, string? description, DateTime expirationDateTime)
    {
        Id = id;
        Title = title;
        Description = description;
        ExpirationDateTime = expirationDateTime;
        PercentageCompletion = 0;

        AddDomainEvent(new ToDoTaskCreatedEvent(Id));
    }


    public static Result<ToDoTask> Create(
        IIdGenerator idGenerator,
        string title,
        string? description,
        DateTime expirationDateTime
    )
    {
        var todoTaskId = ToDoTaskId.Of(idGenerator.NextId());
        if (todoTaskId.IsFailure) return todoTaskId.Error;

        return new ToDoTask(todoTaskId.Value, title, description, expirationDateTime);
    }

    public Result Update(string title, string? description, DateTime expirationDateTime)
    {
        Title = title;
        Description = description;
        ExpirationDateTime = expirationDateTime;

        return Result.Success();
    }

    public Result SetPercentageCompletion(int percentage)
    {
        var result = CheckRules(new ToDoTaskPercentageCompletionMustBeInRange(percentage));
        if (result.IsFailure) return result;

        PercentageCompletion = percentage;

        if (percentage == 100)
            AddDomainEvent(new ToDoTaskCompletedEvent(Id));

        return Result.Success();
    }
}