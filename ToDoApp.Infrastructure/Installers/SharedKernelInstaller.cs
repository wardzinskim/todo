using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoApp.Infrastructure.Abstractions;
using ToDoApp.Infrastructure.SharedKernel;

namespace ToDoApp.Infrastructure.Installers;

public sealed class SharedKernelInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddScoped<IIdGenerator, IdGenerator>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
    }
}