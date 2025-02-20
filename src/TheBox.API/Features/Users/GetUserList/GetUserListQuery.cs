using MediatR;

namespace TheBox.API.Features.Users.GetUserList;

public record GetUserListQuery : IRequest<GetUserListResult>;
