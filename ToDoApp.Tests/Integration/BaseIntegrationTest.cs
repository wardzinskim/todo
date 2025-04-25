using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Infrastructure.Database;

namespace ToDoApp.Tests.Integration;

[CollectionDefinition("ToDoApp collection")]
public class ToDoAppCollection : ICollectionFixture<IntegrationTestWebAppFactory>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

[Collection("ToDoApp collection")]
public class BaseIntegrationTest : IDisposable
{
    private readonly IServiceScope _scope;

    protected readonly HttpClient _httpClient;
    protected readonly ToDoAppContext _dbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory application)
    {
        _httpClient = application.CreateClient();

        _scope = application.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<ToDoAppContext>();
    }

    public void Dispose()
    {
        _scope?.Dispose();
        _dbContext?.Dispose();
    }
}