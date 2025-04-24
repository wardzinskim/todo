using ToDoApp.SharedKernel;

namespace ToDoApp.Domain.ToDoTask.Events;

public record ToDoTaskCreatedEvent(ToDoTaskId ToDoTaskId) : DomainEventBase
{
}