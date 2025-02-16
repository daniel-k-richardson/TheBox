using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using TheBox.Persistence.Users.DatabaseContext;
using Xunit;

namespace TheBox.IntegrationTests;

public abstract class BaseIntegrationTest : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;
    protected IServiceScope ServiceScope;
    protected UserDbContext UserDbContext;
    protected HttpClient Client;

    protected BaseIntegrationTest()
    {
        _container = new PostgreSqlBuilder().Build();
    }

    public virtual async Task InitializeAsync()
    {
        await _container.StartAsync();

        var factory = new FeaturesWebApplicationFactory(_container);
        ServiceScope = factory.Services.CreateScope();
        UserDbContext = ServiceScope.ServiceProvider.GetRequiredService<UserDbContext>();
        Client = factory.CreateClient();

        var db = ServiceScope.ServiceProvider.GetRequiredService<UserDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
        await _container.DisposeAsync();
        ServiceScope.Dispose();
    }
}