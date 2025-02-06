using TheBox.Domain.Users.Entities;

namespace TheBox.UnitTests.Domain.Users;

public sealed class UserTests
{
    [Fact]
    public void CreateUser_ValidParameters_ShouldCreateUser()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "Doe";
        
        // Act
        var user = new User(firstName, lastName);
        
        // Assert   
        Assert.True(user.UserId != Guid.Empty);
        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);
    }
    
    [Fact]
    public void CreateUser_InvalidFirstName_ShouldThrowArgumentException()
    {
        // Arrange
        var firstName = string.Empty;
        const string lastName = "Doe";
        
        // Act and Assert
        var exception = Assert.Throws<ArgumentException>(() => new User(firstName, lastName));
        Assert.Contains("FirstName cannot be empty.", exception.Message);
    }
    
    [Fact]
    public void CreateUser_InvalidLastName_ShouldThrowArgumentException()
    {
        // Arrange
        const string firstName = "John";
        var lastName = string.Empty;
        
        // Act and Assert
        var exception = Assert.Throws<ArgumentException>(() => new User(firstName, lastName));
        Assert.Contains("LastName cannot be empty.", exception.Message);
    }
    
    [Fact]
    public void SetFirstName_ValidParameters_ShouldUpdateFirstName()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "Doe";
        var user = new User(firstName, lastName);
        
        // Act
        const string newFirstName = "Jane";
        user.SetFirstName(newFirstName, lastName);
        
        // Assert
        Assert.Equal(newFirstName, user.FirstName);
    }
    
    [Fact]
    public void SetFirstName_InvalidFirstName_ShouldThrowArgumentException()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "Doe";
        var user = new User(firstName, lastName);
        
        // Act and Assert
        var newFirstName = string.Empty;
        var exception = Assert.Throws<ArgumentException>(() => user.SetFirstName(newFirstName, lastName));
        Assert.Contains("FirstName cannot be empty.", exception.Message);
    }
    
    [Fact]
    public void SetLastName_ValidParameters_ShouldUpdateLastName()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "Doe";
        var user = new User(firstName, lastName);
        
        // Act
        const string newLastName = "Smith";
        user.SetLastName(newLastName);
        
        // Assert
        Assert.Equal(newLastName, user.LastName);
    }
    
    [Fact]
    public void SetLastName_InvalidLastName_ShouldThrowArgumentException()
    {
        // Arrange
        const string firstName = "John";
        const string lastName = "Doe";
        var user = new User(firstName, lastName);
        
        // Act and Assert
        var newLastName = string.Empty;
        var exception = Assert.Throws<ArgumentException>(() => user.SetLastName(newLastName));
        Assert.Contains("LastName cannot be empty.", exception.Message);
    }
}