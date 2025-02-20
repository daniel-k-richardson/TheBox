#region
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using TheBox.Persistence.Users.DatabaseContext;
using Xunit;
#endregion

namespace TheBox.IntegrationTests;

public abstract class BaseIntegrationTest : IAsyncLifetime
{
    readonly PostgreSqlContainer _container;
    protected HttpClient Client;
    protected IServiceScope ServiceScope;
    protected UserDbContext UserDbContext;

    protected BaseIntegrationTest()
    {
        _container = new PostgreSqlBuilder().Build();
        ServiceScope = null!;
        UserDbContext = null!;
        Client = null!;
    }

    public virtual async Task InitializeAsync()
    {
        await _container.StartAsync();

        var factory = new WebApplicationFactorySetup(_container);
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
