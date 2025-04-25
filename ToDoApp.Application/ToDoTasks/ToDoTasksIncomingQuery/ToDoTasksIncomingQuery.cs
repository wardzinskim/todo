using MassTransit.Mediator;
using ToDoApp.Application.ToDoTasks.Model;
using ToDoApp.SharedKernel;

namespace ToDoApp.Application.ToDoTasks.ToDoTasksIncomingQuery;

public record ToDoTasksIncomingQuery(IncomingType Type) : Request<Result<IEnumerable<ToDoTaskDTO>>>;

public enum IncomingType
{
    Today,
    NextDay,
    CurrentWeek
}