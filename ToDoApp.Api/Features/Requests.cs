namespace ToDoApp.Api.Features;

public record ToDoTaskRequest(string Title, string? Description, DateTime ExpirationDateTime);

public record ToDoTaskSetPercentageCompletionRequest(int PercentageCompletion);