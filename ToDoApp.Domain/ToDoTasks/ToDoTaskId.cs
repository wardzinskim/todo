using ToDoApp.Domain.ToDoTasks.Rules;
using ToDoApp.SharedKernel;

namespace ToDoApp.Domain.ToDoTasks;

public record ToDoTaskId : ValueObject
{
    public Guid Value { get; }

    private ToDoTaskId(Guid value)
    {
        Value = value;
    }

    public static Result<ToDoTaskId> Of(Guid value)
    {
        var result = CheckRules(new ToDoTaskIdMustNotBeEmpty(value));

        if (result.IsFailure)
        {
            return result.Error;
        }

        return new ToDoTaskId(value);
    }

    public static implicit operator Guid(ToDoTaskId orderId) => orderId.Value;
}