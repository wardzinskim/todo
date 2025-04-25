using ToDoApp.SharedKernel;

namespace ToDoApp.Tests.Mocks;

internal class IdGeneratorMock(Guid Id) : IIdGenerator
{
    public Guid NextId() => Id;
}