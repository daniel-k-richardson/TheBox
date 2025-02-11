using TheBox.API.Features.Users.CreateUser;

namespace TheBox.UnitTests.API.Users.CreateUser;

public class CreateUserValidationTest
{
    private readonly CreateUserValidation _validator = new();

    [Fact]
    public void Validate_WhenFirstNameAndLastNameAreNotEmpty_ShouldReturnSuccess()
    {
        // Arrange
        var user = new CreateUserCommand("john", "doe");

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WhenFirstNameIsEmpty_ShouldReturnError()
    {
        // Arrange
        var user = new CreateUserCommand("", "doe");

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("must not be empty", result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void Validate_WhenLastNameIsEmpty_ShouldReturnError()
    {
        // Arrange
        var user = new CreateUserCommand("john", "");

        // Act
        var result = _validator.Validate(user);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains("must not be empty", result.Errors[0].ErrorMessage);
    }
}