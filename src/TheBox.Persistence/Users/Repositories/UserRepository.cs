using TheBox.Domain.Users.Entities;
using TheBox.Domain.Users.Exceptions;
using TheBox.Domain.Users.Interfaces;
using TheBox.Persistence.Users.DatabaseContext;

namespace TheBox.Persistence.Users.Repositories;

public sealed class UserRepository(UserDbContext userDbContext) : IUserRepository
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await userDbContext.SaveChangesAsync(cancellationToken);
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

    public async Task DeleteAsync(UserId id)
    {
        var user = await userDbContext.Users.FindAsync(id);
        if (user is null)
        {
            throw new UserNotFoundException("User not found.");
        }

        userDbContext.Users.Remove(user);
        await userDbContext.SaveChangesAsync();
    }
}
