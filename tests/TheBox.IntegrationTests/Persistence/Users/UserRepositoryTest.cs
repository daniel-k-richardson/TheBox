#region
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TheBox.Domain.Users.Entities;
using TheBox.Domain.Users.Exceptions;
using TheBox.Domain.Users.Interfaces;
using TheBox.TestUtils.ObjectMothers;
using Xunit;
#endregion

namespace TheBox.IntegrationTests.Persistence.Users;

public class UserRepositoryTest : BaseIntegrationTest
{
    IUserRepository _userRepository = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _userRepository = ServiceScope.ServiceProvider.GetRequiredService<IUserRepository>();
    }

    [Fact]
    public async Task AddUser_WithValidInputs_SavesSuccessfully()
    {
        // Arrange
        var user = UserMother.Create();

        // Act
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        // Assert
        var createdUser = await UserDbContext.Users.SingleAsync();
        Assert.Equal(user.FirstName, createdUser.FirstName);
        Assert.Equal(user.LastName, createdUser.LastName);
    }

    [Fact]
    public async Task UpdateUser_WithInputs_UpdatedSuccessfully()
    {
        // Arrange
        var user = UserMother.Create();
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        user.SetFirstName("David");

        // Act
        await _userRepository.UpdateAsync(user);

        // Assert
        var updatedUser = await UserDbContext.Users.SingleAsync();
        Assert.Equal(user.FirstName, updatedUser.FirstName);
        Assert.Equal(user.LastName, updatedUser.LastName);
    }

    [Fact]
    public async Task DeleteUser_WithValidId_DeletedSuccessfully()
    {
        // Arrange
        var user = UserMother.Create();
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        // Act
        await _userRepository.DeleteAsync(user.Id);

        // Assert
        var deletedUser = await UserDbContext.Users.FindAsync(user.Id);
        Assert.Null(deletedUser);
    }

    [Fact]
    public async Task DeleteUser_WithInvalidId_UserNotFound()
    {
        // Arrange
        var userId = new UserId();

        // Act and assert
        await Assert.ThrowsAsync<UserNotFoundException>(async () => await _userRepository.DeleteAsync(userId));
    }
}
