using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.ToDoTasks;
using ToDoApp.Infrastructure.Database;

namespace ToDoApp.Infrastructure.Domain.ToDoTasks;

internal class ToDoTaskRepository(ToDoAppContext context) : IToDoTaskRepository
{
    private readonly ToDoAppContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task AddAsync(ToDoTask task, CancellationToken cancellationToken = default)
        => await _context.ToDoTasks.AddAsync(task, cancellationToken);

    public async Task<ToDoTask?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.ToDoTasks
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

    public void Delete(ToDoTask todoTask)
        => _context.ToDoTasks.Remove(todoTask);
}