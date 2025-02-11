using FluentValidation;
using TheBox.API.Features.Users.CreateUser;
using TheBox.Domain.Users.Entities;

namespace TheBox.UnitTests.API.Users.CreateUser;

public class CreateUserValidationTest
{
    private readonly CreateUserValidation _validator = new();
    
    [Fact]
    public void Validate_WhenFirstNameAndLastNameAreNotEmpty_ShouldReturnSuccess()
    {
        var user = new CreateUserCommand("john", "doe");
            
        var result = _validator.Validate(user);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WhenFirstNameIsEmpty_ShouldReturnError()
    {
        var user = new CreateUserCommand("", "doe");
            
        var result = _validator.Validate(user);

        Assert.False(result.IsValid);

        Assert.Contains("must not be empty", result.Errors[0].ErrorMessage);
    }
    
    [Fact]
    public void Validate_WhenLastNameIsEmpty_ShouldReturnError()
    {
        var user = new CreateUserCommand("john", "");
            
        var result = _validator.Validate(user);

        Assert.False(result.IsValid);

        Assert.Contains("must not be empty", result.Errors[0].ErrorMessage);
    }
}