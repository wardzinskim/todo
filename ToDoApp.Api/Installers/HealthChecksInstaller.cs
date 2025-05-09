﻿using ToDoApp.Infrastructure.Abstractions;

namespace ToDoApp.Api.Installers;

public sealed class HealthChecksInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        var healthCheckBuilder = services.AddHealthChecks();

        healthCheckBuilder.AddNpgSql(configuration.GetConnectionString("Default")!);
    }

    public static void Use(WebApplication app)
    {
        app.MapHealthChecks("/health");
    }
}