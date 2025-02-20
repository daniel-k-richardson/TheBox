using MediatR;
using TheBox.Domain.Users.Entities;

namespace TheBox.API.Features.Users.GetUser;

public record GetUserQuery(UserId Id) : IRequest<GetUserResult>;
