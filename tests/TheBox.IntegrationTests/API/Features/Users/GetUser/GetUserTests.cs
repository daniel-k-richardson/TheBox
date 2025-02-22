using System.Net;
using System.Net.Http.Json;
using TheBox.API.Features.Users.GetUser;
using TheBox.TestUtils.ObjectMothers;
using Xunit;

namespace TheBox.IntegrationTests.API.Features.Users.GetUser;

public class GetUserTests : BaseIntegrationTest
{
    [Fact]
    public async Task GetUser_WithValidId_ReturnsUser()
    {
        // Arrange
        var user = UserMother.Create();
        await UserDbContext.Users.AddAsync(user);
        await UserDbContext.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync($"/api/users/{user.Id.Value}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var getUserResult = await response.Content.ReadFromJsonAsync<GetUserResult>();

        // Assert
        Assert.NotNull(getUserResult);
        Assert.Equal(user.Id.Value, getUserResult.UserId);
        Assert.Equal(user.FirstName, getUserResult.FirstName);
        Assert.Equal(user.LastName, getUserResult.LastName);
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
