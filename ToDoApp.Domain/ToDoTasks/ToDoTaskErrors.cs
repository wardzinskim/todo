using ToDoApp.SharedKernel;

namespace ToDoApp.Domain.ToDoTasks;

public static class ToDoTaskErrors
{
    public static readonly Error ToDoTaskNotFound =
        new NotFoundError(nameof(ToDoTaskNotFound), "ToDoTask with the specified id not exists.");
}