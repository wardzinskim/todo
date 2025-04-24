using ToDoApp.SharedKernel;

namespace ToDoApp.Domain.ToDoTasks.Events;

public record ToDoTaskCompletedEvent(ToDoTaskId ToDoTaskId) : DomainEventBase
{
}