using MassTransit;

namespace ToDoApp.Infrastructure.SharedKernel;

public sealed class IdGenerator : IIdGenerator
{
    public Guid NextId() => NewId.NextSequentialGuid();
}