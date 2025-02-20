#region
using MediatR;
using TheBox.Domain.Users.Entities;
#endregion

namespace TheBox.API.Features.Users.GetUser;

public record GetUserQuery(UserId Id) : IRequest<GetUserResult>;
