using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDoApp.Api.Installers;
using ToDoApp.Api.Installers.ExceptionHandlers;
using ToDoApp.Infrastructure.Abstractions;
using ToDoApp.Infrastructure.Database;

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
            .Install(typeof(Program).Assembly)
            .Install(typeof(ToDoAppContext).Assembly);

        var app = builder.Build();

        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        ExceptionHandlersInstaller.Use(app);
        HealthChecksInstaller.Use(app);
        SwaggerInstaller.Use(app);
        CarterInstaller.Use(app);


        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ToDoAppContext>();
            dbContext.Database.Migrate();
        }

        app.Run();
    }
}