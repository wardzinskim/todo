using ToDoApp.SharedKernel;

namespace ToDoApp.Domain.ToDoTasks.Events;

public record ToDoTaskCreatedEvent(ToDoTaskId ToDoTaskId) : DomainEventBase
{
}