namespace ToDoApp.SharedKernel;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}