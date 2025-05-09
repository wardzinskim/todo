﻿using Microsoft.AspNetCore.Diagnostics;
using ToDoApp.Infrastructure.Abstractions;

namespace ToDoApp.Api.Installers.ExceptionHandlers;

public sealed class ExceptionHandlersInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance =
                    context.HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Path;
                context.ProblemDetails.Extensions.Add("trace-id", context.HttpContext.TraceIdentifier);
            };
        });
    }

    public static void Use(WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseStatusCodePages();
    }
}