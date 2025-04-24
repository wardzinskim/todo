using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.ToDoTasks;
using ToDoApp.Infrastructure.Database.Converters;

namespace ToDoApp.Infrastructure.Database;

public class ToDoAppContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ToDoTask> ToDoTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoAppContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder
            .Properties<ToDoTaskId>()
            .HaveConversion<ToDoTaskIdConverter>();
    }
}