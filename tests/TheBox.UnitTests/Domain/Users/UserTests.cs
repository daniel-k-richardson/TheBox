using TheBox.Domain.Users.Entities;

namespace TheBox.UnitTests.Domain.Users;

public sealed class UserTests
{
    [Fact]
    public void CreateUser_ValidInputs_UserCreated()
    {
        // Arrange
        var user = new User("John", "Doe");

        // Act
        var firstName = user.FirstName;
        var lastName = user.LastName;

        // Assert
        Assert.Equal("John", firstName);
        Assert.Equal("Doe", lastName);
    }

    [Fact]
    public void CreateUser_EmptyFirstName_ThrowsException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new User("", "Doe"));
        Assert.Contains("cannot be empty.", exception.Message);
    }

    [Fact]
    public void CreateUser_EmptyLastName_ThrowsException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new User("John", ""));
        Assert.Contains("cannot be empty.", exception.Message);
    }

    [Fact]
    public void CreateUser_NullFirstName_ThrowsException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new User(null, "Doe"));
        Assert.Contains("cannot be empty", exception.Message);
    }

    [Fact]
    public void CreateUser_NullLastName_ThrowsException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new User("John", null));
        Assert.Contains("cannot be empty", exception.Message);
    }
}