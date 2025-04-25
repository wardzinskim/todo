namespace ToDoApp.Application.ToDoTasks.Model;

public record ToDoTaskDTO(
    Guid Id,
    string Title,
    string? Description,
    DateTime? ExpirationDateTime,
    int PercentageComplete
);