#region
using System.Net;
using System.Text.Json;
using TheBox.Domain.Users.Entities;
using Xunit;
#endregion

namespace TheBox.IntegrationTests.API.Features.Users.GetUser;

public class GetUserTests : BaseIntegrationTest
{

    [Fact]
    public async Task GetUser_WithValidId_ReturnsUser()
    {
        // Arrange
        var user = new User("John", "Doe");
        await UserDbContext.Users.AddAsync(user);
        await UserDbContext.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync($"/api/users/{user.Id.Value}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var userResponse = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.NotNull(userResponse);
        Assert.Equal(user.FirstName, userResponse.FirstName);
        Assert.Equal(user.LastName, userResponse.LastName);
    }

    [Fact]
    public async Task GetUser_WithInvalidId_UserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"/api/users/{userId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Contains("The user was not found", await response.Content.ReadAsStringAsync());
    }
}
