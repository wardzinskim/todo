namespace ToDoApp.Domain.ToDoTasks;

public interface IToDoTaskRepository
{
    Task AddAsync(ToDoTask task, CancellationToken cancellationToken = default);
    Task<ToDoTask?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}