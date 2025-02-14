using Microsoft.Extensions.DependencyInjection;
using TheBox.Persistence.Users.DatabaseContext;
using Xunit;

namespace TheBox.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<FeaturesWebApplicationFactory>
{
    protected readonly HttpClient _client;
    readonly IServiceScope _serviceScope;
    protected readonly UserDbContext _userDbContext;

    protected BaseIntegrationTest(FeaturesWebApplicationFactory factory)
    {
        _serviceScope = factory.Services.CreateScope();
        _client = factory.CreateClient();
        _userDbContext = _serviceScope.ServiceProvider.GetRequiredService<UserDbContext>();
    }
}