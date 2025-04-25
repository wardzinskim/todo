using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.ToDoTasks.Model;
using ToDoApp.Application.ToDoTasks.ToDoTasksIncomingQuery;
using ToDoApp.Infrastructure.Database;

namespace ToDoApp.Infrastructure.Application.ToDoTasks;

public class ToDoTasksIncomingQueryHandler(
    ToDoAppContext db,
    IDateTimeProvider dateTimeProvider
)
    : MediatorRequestHandler<ToDoTasksIncomingQuery, Result<IEnumerable<ToDoTaskDTO>>>
{
    protected override async Task<Result<IEnumerable<ToDoTaskDTO>>> Handle(
        ToDoTasksIncomingQuery request,
        CancellationToken cancellationToken
    )
    {
        var dateRange = GetDateRange(request.Type);

        var result = await db.ToDoTasks
            .Where(x => x.ExpirationDateTime >= dateRange.StartDate && x.ExpirationDateTime < dateRange.EndDate)
            .Select(x =>
                new ToDoTaskDTO(x.Id, x.Title, x.Description, x.ExpirationDateTime, x.PercentageCompletion))
            .ToListAsync(cancellationToken: cancellationToken);

        return Result.Success(result.AsEnumerable());
    }


    private (DateTime StartDate, DateTime EndDate) GetDateRange(IncomingType type)
    {
        DateTime startDate;
        DateTime endDate;

        switch (type)
        {
            case IncomingType.Today:
                startDate = dateTimeProvider.UtcNow.Date;
                endDate = startDate.AddDays(1);
                break;
            case IncomingType.NextDay:
                startDate = dateTimeProvider.UtcNow.Date.AddDays(1);
                endDate = startDate.AddDays(2);
                break;
            case IncomingType.CurrentWeek:
                DateTime today = DateTime.UtcNow.Date;
                DayOfWeek currentDay = today.DayOfWeek;

                int daysToSubtract = (currentDay == DayOfWeek.Sunday ? 7 : (int)currentDay) - 1;
                startDate = today.AddDays(-daysToSubtract);
                endDate = startDate.AddDays(7);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return (startDate, endDate);
    }
}