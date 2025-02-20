#region
using TheBox.Domain.Users.Entities;
#endregion

namespace TheBox.Domain.Users.Interfaces;

public interface IUserRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(UserId id);
}
