using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using ToDoApp.Api;
using ToDoApp.Infrastructure.Database;

namespace ToDoApp.Tests.Integration;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.4-alpine3.20")
        .Build();


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor =
                services.SingleOrDefault(service => typeof(DbContextOptions<ToDoAppContext>) == service.ServiceType);
            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContextPool<ToDoAppContext>(dbContextOptions =>
            {
                dbContextOptions
                    .UseNpgsql(_postgreSqlContainer.GetConnectionString(), postgresoptions =>
                    {
                        postgresoptions.MigrationsAssembly(typeof(ToDoAppContext).Assembly.FullName);
                    })
                    .UseSnakeCaseNamingConvention();
            });
        });
    }


    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        using var scope = Services.CreateScope();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _postgreSqlContainer.StopAsync();
    }
}