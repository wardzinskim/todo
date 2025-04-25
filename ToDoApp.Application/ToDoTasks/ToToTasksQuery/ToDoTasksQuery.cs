using MassTransit.Mediator;
using ToDoApp.Application.ToDoTasks.Model;
using ToDoApp.SharedKernel;

namespace ToDoApp.Application.ToDoTasks.ToToTasksQuery;

public record ToDoTasksQuery() : Request<Result<IEnumerable<ToDoTaskDTO>>>;