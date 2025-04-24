using MassTransit;
using ToDoApp.Api.Installers.MediatorFilters;
using ToDoApp.Infrastructure.Abstractions;

namespace ToDoApp.Api.Installers;

public sealed class MediatorInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddMediator(configure =>
        {
            // configure.AddConsumers(typeof(CreateBudgetCommandHandler).Assembly);
            // configure.AddConsumers(typeof(GetBudgetsQueryHandler).Assembly);

            configure.ConfigureMediator((context, cfg) =>
            {
                cfg.UseConsumeFilter(typeof(ValidationFilter<>), context);
            });
        });
    }
}