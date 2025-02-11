using MediatR;
using TheBox.Domain.Users.Entities;

namespace TheBox.API.Features.Users;

public record GetUserQuery(UserId Id) : IRequest<GetUserResult>;