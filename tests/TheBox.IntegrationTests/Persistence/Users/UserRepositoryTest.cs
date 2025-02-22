using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TheBox.Domain.Users.Entities;
using TheBox.Domain.Users.Exceptions;
using TheBox.Domain.Users.Interfaces;
using TheBox.TestUtils.ObjectMothers;
using Xunit;

namespace TheBox.IntegrationTests.Persistence.Users;

public class UserRepositoryTest : BaseIntegrationTest
{
    private IUserRepository userRepository = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        userRepository = ServiceScope.ServiceProvider.GetRequiredService<IUserRepository>();
    }

    [Fact]
    public async Task AddUser_WithValidInputs_SavesSuccessfully()
    {
        // Arrange
        var user = UserMother.Create();

        // Act
        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();

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
        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();

        user.SetFirstName("David");

        // Act
        await userRepository.UpdateAsync(user);

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
        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();

        // Act
        await userRepository.DeleteAsync(user.Id);

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
        await Assert.ThrowsAsync<UserNotFoundException>(async () => await userRepository.DeleteAsync(userId));
    }
}
