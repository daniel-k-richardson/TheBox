using TheBox.Domain.Users.Entities;

namespace TheBox.Domain.Users.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid userId);
}