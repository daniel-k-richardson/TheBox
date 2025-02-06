using TheBox.Domain.Users.Entities;
using TheBox.Domain.Users.Exceptions;
using TheBox.Domain.Users.Interfaces;
using TheBox.Persistence.Users.DatabaseContext;

namespace TheBox.Persistence.Users.Repositories;

public sealed class UserRepository(UserDbContext userDbContext) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await userDbContext.Users.FindAsync(userId);
    }

    public async Task<User> AddAsync(User user)
    {
        await userDbContext.Users.AddAsync(user);
        await userDbContext.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        userDbContext.Update(user);
        await userDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid userId)
    {
        var user = await GetByIdAsync(userId);
        if (user is null)
        {
            throw new UserNotFoundException("User not found.");
        }
        
        userDbContext.Users.Remove(user);
        await userDbContext.SaveChangesAsync();
    }
}