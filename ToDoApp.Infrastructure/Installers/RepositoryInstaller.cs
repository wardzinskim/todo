using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoApp.Domain.ToDoTasks;
using ToDoApp.Infrastructure.Abstractions;
using ToDoApp.Infrastructure.Domain.ToDoTasks;

namespace ToDoApp.Infrastructure.Installers;

public sealed class RepositoryInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddTransient<IToDoTaskRepository, ToDoTaskRepository>();
    }
}