using TheBox.Domain.Users.Entities;

namespace TheBox.Domain.Users.Interfaces;

public interface IUserRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid userId);
}