using System.Text;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using TheBox.API.Features.Users.CreateUser;
using Xunit;

namespace TheBox.IntegrationTests.API.Features.Users.CreateUser;

public class CreateUserTests : IClassFixture<FeaturesWebApplicationFactory<Program>>
{
    
    private readonly HttpClient _client;

    public CreateUserTests(FeaturesWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task CreateUser_WhenFirstNameAndLastNameAreNotEmpty_ShouldReturnSuccess()
    {
        // Arrange
        var user = new CreateUserCommand("john", "doe");
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/users", content);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}