using TheBox.Domain.Users.Entities;
using TheBox.Domain.Users.Exceptions;
using TheBox.Domain.Users.Interfaces;
using TheBox.Persistence.Users.DatabaseContext;

namespace TheBox.Persistence.Users.Repositories;

public sealed class UserRepository(UserDbContext userDbContext) : IUserRepository
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await userDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
    {
        await userDbContext.Users.AddAsync(user, cancellationToken);
        await userDbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        userDbContext.Update(user);
        await userDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(UserId id, CancellationToken cancellationToken)
    {
        var user = await userDbContext.Users.FindAsync(id, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException("User not found.");
        }

        userDbContext.Users.Remove(user);
        await userDbContext.SaveChangesAsync(cancellationToken);
    }
}
