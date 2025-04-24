namespace ToDoApp.SharedKernel;

public interface IIdGenerator
{
    Guid NextId();
}