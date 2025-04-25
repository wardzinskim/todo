namespace ToDoApp.Infrastructure.SharedKernel;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}