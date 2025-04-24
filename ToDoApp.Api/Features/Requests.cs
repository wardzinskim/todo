namespace ToDoApp.Api.Features;

public record CreateToDoTaskRequest(string Title, string? Description, DateTime ExpirationDateTime);