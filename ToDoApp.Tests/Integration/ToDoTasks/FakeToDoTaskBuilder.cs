using ToDoApp.Domain.ToDoTasks;
using ToDoApp.Tests.Mocks;

namespace ToDoApp.Tests.Integration.ToDoTasks;

public class FakeToDoTaskBuilder
{
    public static ToDoTask Build(Guid id, string title, string? description, DateTime expirationDateTime)
    {
        var response = ToDoTask
            .Create(new IdGeneratorMock(id), title, description, expirationDateTime);
        return response.Value;
    }
}