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
        Assert.Equal("First name cannot be empty.", exception.Message);
    }

    [Fact]
    public void CreateUser_EmptyLastName_ThrowsException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new User("John", ""));
        Assert.Equal("Last name cannot be empty.", exception.Message);
    }

    [Fact]
    public void CreateUser_NullFirstName_ThrowsException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => new User(null!, "Doe"));
        Assert.Equal("Value cannot be null. (Parameter 'firstName')", exception.Message);
    }

    [Fact]
    public void CreateUser_NullLastName_ThrowsException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => new User("John", null!));
        Assert.Equal("Value cannot be null. (Parameter 'lastName')", exception.Message);
    }
}