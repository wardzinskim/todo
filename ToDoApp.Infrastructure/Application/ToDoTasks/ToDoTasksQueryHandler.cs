using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.ToDoTasks.Model;
using ToDoApp.Application.ToDoTasks.ToToTasksQuery;
using ToDoApp.Infrastructure.Database;

namespace ToDoApp.Infrastructure.Application.ToDoTasks;

public class ToDoTasksQueryHandler(
    ToDoAppContext db
) : MediatorRequestHandler<ToDoTasksQuery, Result<IEnumerable<ToDoTaskDTO>>>
{
    protected override async Task<Result<IEnumerable<ToDoTaskDTO>>> Handle(
        ToDoTasksQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await db.ToDoTasks
            .Select(x =>
                new ToDoTaskDTO(x.Id, x.Title, x.Description, x.ExpirationDateTime, x.PercentageCompletion))
            .ToListAsync(cancellationToken: cancellationToken);

        return Result.Success(result.AsEnumerable());
    }
}