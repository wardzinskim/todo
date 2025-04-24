using ToDoApp.Application.Abstractions;

namespace ToDoApp.Infrastructure.Database;

internal class UnitOfWork(ToDoAppContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}