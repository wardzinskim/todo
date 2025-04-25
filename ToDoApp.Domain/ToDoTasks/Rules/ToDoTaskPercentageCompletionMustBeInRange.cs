using ToDoApp.SharedKernel;

namespace ToDoApp.Domain.ToDoTasks.Rules;

public class ToDoTaskPercentageCompletionMustBeInRange(int percentage) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(ToDoTaskPercentageCompletionMustBeInRange),
            "Percentage must be between 0 and 100.");

    public Result Validate()
    {
        if (percentage is >= 0 and <= 100) return Result.Success();
        return Result.Failure(Error);
    }
}