using TheBox.Domain.Users.Entities;

namespace TheBox.API.Features.Users;

public record GetUserResult(UserId Id, string FirstName, string LastName);