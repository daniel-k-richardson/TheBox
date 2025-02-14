using System.Net;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using TheBox.API.Features.Users.CreateUser;
using Xunit;

namespace TheBox.IntegrationTests.API.Features.Users.CreateUser;

public class CreateUserTests(FeaturesWebApplicationFactory factory) : BaseIntegrationTest(factory)
{

    [Fact]
    public async Task CreateUser_WhenFirstNameAndLastNameAreNotEmpty_ShouldReturnSuccess()
    {
        // Arrange
        var user = GetValidCreateUserCommand();
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        // Act
        var response = await this._client.PostAsync("/api/users", content);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateUser_WhenFirstNameIsEmpty_ShouldReturnValidationError()
    {
        // Arrange
        var user = GetValidCreateUserCommand(string.Empty);

        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        // Act
        var response = await this._client.PostAsync("/api/users", content);
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string[]>>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("FirstName", result.Keys);
        Assert.Contains("must not be empty", result["FirstName"].Single());
    }

    static CreateUserCommand GetValidCreateUserCommand(string firstName = "john", string lastName = "doe")
    {
        return new CreateUserCommand(firstName, lastName);
    }
}