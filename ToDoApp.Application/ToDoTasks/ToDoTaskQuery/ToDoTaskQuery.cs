using MassTransit.Mediator;
using ToDoApp.Application.ToDoTasks.Model;
using ToDoApp.SharedKernel;

namespace ToDoApp.Application.ToDoTasks.ToDoTaskQuery;

public record ToDoTaskQuery(Guid Id) : Request<Result<ToDoTaskDTO>>;