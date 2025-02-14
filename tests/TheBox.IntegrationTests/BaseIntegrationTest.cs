using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace TheBox.IntegrationTests;

public abstract class BaseIntegrationTest(FeaturesWebApplicationFactory factory) : IClassFixture<FeaturesWebApplicationFactory>
{
    protected readonly HttpClient _client = factory.CreateClient();
    readonly IServiceScope _serviceScope = factory.Services.CreateScope();
}