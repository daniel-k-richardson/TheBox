#region
using TheBox.Domain.Users.Entities;
#endregion

namespace TheBox.API.Features.Users.GetUser;

public record GetUserResult(UserId Id, string FirstName, string LastName);
