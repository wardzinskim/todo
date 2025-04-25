using MassTransit;
using ToDoApp.Api.Installers.MediatorFilters;
using ToDoApp.Application.ToDoTasks.CreateToDoTaskCommand;
using ToDoApp.Infrastructure.Abstractions;
using ToDoApp.Infrastructure.Application.ToDoTasks;

namespace ToDoApp.Api.Installers;

public sealed class MediatorInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddMediator(configure =>
        {
            configure.AddConsumers(typeof(CreateToDoTaskCommand).Assembly);
            configure.AddConsumers(typeof(ToDoTasksQueryHandler).Assembly);

            configure.ConfigureMediator((context, cfg) =>
            {
                cfg.UseConsumeFilter(typeof(ValidationFilter<>), context);
            });
        });
    }
}