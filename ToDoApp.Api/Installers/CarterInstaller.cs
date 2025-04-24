using Carter;
using ToDoApp.Infrastructure.Abstractions;

namespace ToDoApp.Api.Installers;

public sealed class CarterInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddCarter();
    }

    public static void Use(WebApplication app)
    {
        app.MapCarter();
    }
}