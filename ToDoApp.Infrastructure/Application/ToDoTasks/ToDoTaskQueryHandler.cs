using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.ToDoTasks.Model;
using ToDoApp.Application.ToDoTasks.ToDoTaskQuery;
using ToDoApp.Domain.ToDoTasks;
using ToDoApp.Infrastructure.Database;

namespace ToDoApp.Infrastructure.Application.ToDoTasks;

public class ToDoTaskQueryHandler(
    ToDoAppContext db
) : MediatorRequestHandler<ToDoTaskQuery, Result<ToDoTaskDTO>>
{
    protected override async Task<Result<ToDoTaskDTO>> Handle(
        ToDoTaskQuery request,
        CancellationToken cancellationToken
    )
    {
        var todoTask = await db.ToDoTasks
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (todoTask is null) return ToDoTaskErrors.ToDoTaskNotFound;

        return Result.Success(new ToDoTaskDTO(
            todoTask.Id,
            todoTask.Title,
            todoTask.Description,
            todoTask.ExpirationDateTime,
            todoTask.PercentageCompletion
        ));
    }
}