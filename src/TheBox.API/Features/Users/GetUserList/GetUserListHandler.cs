using MediatR;
using Microsoft.EntityFrameworkCore;
using TheBox.API.Features.Users.GetUser;
using TheBox.Persistence.Users.DatabaseContext;

namespace TheBox.API.Features.Users.GetUserList;

public class GetUserListHandler(UserDbContext userContext) : IRequestHandler<GetUserListQuery, GetUserListResult>
{
    public async Task<GetUserListResult> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var users = await userContext.Users.ToListAsync(cancellationToken);

        return new GetUserListResult(users.Select(user =>
            new GetUserResult(user.Id.Value, user.FirstName, user.LastName)));
    }
}
