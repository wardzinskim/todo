using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoApp.Domain.ToDoTasks;

namespace ToDoApp.Infrastructure.Database.Converters;

public class ToDoTaskIdConverter()
    : ValueConverter<ToDoTaskId, Guid>(id => id.Value, value => ToDoTaskId.Of(value).Value);