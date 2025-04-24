using Serilog;
using ToDoApp.Api.IInstallers;
using ToDoApp.Api.IInstallers.ExceptionHandlers;
using ToDoApp.Infrastructure.Abstractions;

namespace ToDoApp.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(builder.Configuration);
        });

        builder
            .Install(typeof(Program).Assembly);

        var app = builder.Build();

        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        ExceptionHandlersInstaller.Use(app);
        HealthChecksInstaller.Use(app);
        SwaggerInstaller.Use(app);
        CarterInstaller.Use(app);
        app.Run();
    }
}