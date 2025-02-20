using System.Net;
using System.Net.Http.Json;
using TheBox.API.Features.Users.GetUserList;
using TheBox.TestUtils.ObjectMothers;
using Xunit;

namespace TheBox.IntegrationTests.API.Features.Users.GetUserList;

public class GetUserListTests : BaseIntegrationTest
{

    [Fact]
    public async Task GetUserList__ReturnsUsers()
    {
        // Arrange
        var users = UserMother.CreateList(2);
        await UserDbContext.AddRangeAsync(users);
        await UserDbContext.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync("/api/users");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var getUserListResult = await response.Content.ReadFromJsonAsync<GetUserListResult>();

        // Assert
        Assert.NotNull(getUserListResult);
        Assert.NotEmpty(getUserListResult.Users);
        Assert.Equal(2, getUserListResult.Users.Count);

        // verify that the users returned are the same as the one created
        var user = users.First();
        Assert.Contains(user.Id.Value, getUserListResult.Users.Select(u => u.UserId));
        Assert.Contains(user.FirstName, getUserListResult.Users.Select(u => u.FirstName));
        Assert.Contains(user.LastName, getUserListResult.Users.Select(u => u.LastName));
    }
}
