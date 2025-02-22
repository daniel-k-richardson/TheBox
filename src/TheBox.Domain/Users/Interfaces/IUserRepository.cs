using TheBox.Domain.Users.Entities;

namespace TheBox.Domain.Users.Interfaces;

public interface IUserRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<User> AddAsync(User user, CancellationToken cancellationToken = default);

    Task UpdateAsync(User user, CancellationToken cancellationToken = default);

    Task DeleteAsync(UserId id, CancellationToken cancellationToken = default);
}
