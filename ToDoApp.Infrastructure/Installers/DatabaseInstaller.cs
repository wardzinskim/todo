using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoApp.Application.Abstractions;
using ToDoApp.Infrastructure.Abstractions;
using ToDoApp.Infrastructure.Database;

namespace ToDoApp.Infrastructure.Installers;

public sealed class DatabaseInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddDbContextPool<ToDoAppContext>((sp, dbContextOptions) =>
        {
            dbContextOptions
                .UseNpgsql(configuration.GetConnectionString("Default"), c =>
                {
                    c.MigrationsHistoryTable("__EFMigrationsHistory", SchemaName.ToDoApp);
                })
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}