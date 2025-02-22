using System.Net;
using System.Net.Http.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TheBox.API.Features.Users.CreateUser;
using Xunit;

namespace TheBox.IntegrationTests.API.Features.Users.CreateUser;

public class CreateUserTests : BaseIntegrationTest
{
    [Fact]
    public async Task CreateUser_WhenFirstNameAndLastNameAreNotEmpty_ShouldReturnSuccess()
    {
        // Arrange
        var user = GetValidCreateUserCommand();
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/api/users", content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // verify the user was created
        var createdUser = await UserDbContext.Users.SingleAsync();
        Assert.Equal(user.FirstName, createdUser.FirstName);
        Assert.Equal(user.LastName, createdUser.LastName);
    }

    // check validation is properly setup
    [Fact]
    public async Task CreateUser_WhenFirstNameIsEmpty_ShouldReturnValidationError()
    {
        // Arrange
        var user = GetValidCreateUserCommand() with { FirstName = string.Empty };
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        // Act
        var response = await Client.PostAsync("/api/users", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        // verify the response contains the validation error
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string[]>>();
        Assert.NotNull(result);
        Assert.Contains("FirstName", result.Keys);
        Assert.Contains("must not be empty", result["FirstName"].Single());

        // verify the user was not created
        Assert.Empty(await UserDbContext.Users.ToListAsync());
    }

    private static CreateUserCommand GetValidCreateUserCommand(string firstName = "john", string lastName = "doe")
    {
        return new CreateUserCommand(firstName, lastName);
    }
}
