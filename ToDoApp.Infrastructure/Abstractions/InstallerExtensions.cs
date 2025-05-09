﻿using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace ToDoApp.Infrastructure.Abstractions;

public static class InstallerExtensions
{
    public static IHostApplicationBuilder Install(
        this IHostApplicationBuilder builder,
        Assembly? assembly
    )
    {
        if (assembly is null) return builder;

        var installers = assembly.ExportedTypes
            .Where(t => typeof(IInstaller).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>()
            .ToList();


        installers.ForEach(installer =>
            installer.Install(builder.Services, builder.Configuration, builder.Environment));


        return builder;
    }
}