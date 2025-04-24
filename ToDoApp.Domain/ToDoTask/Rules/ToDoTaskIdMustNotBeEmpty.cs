using ToDoApp.SharedKernel;

namespace ToDoApp.Domain.ToDoTask.Rules;

internal class ToDoTaskIdMustNotBeEmpty(Guid value) : IBusinessRule
{
    private static readonly Error Error =
        new BusinessRuleValidationError(nameof(ToDoTaskIdMustNotBeEmpty),
            "BudgetId must not be empty.");

    public Result Validate()
    {
        if (value != Guid.Empty) return Result.Success();
        return Result.Failure(Error);
    }
}