using MediatR;
using TheBox.Domain.Users.Exceptions;
using TheBox.Persistence.Users.DatabaseContext;

namespace TheBox.API.Features.Users.GetUser;

public sealed class GetUserHandler(UserDbContext userDbContext) : IRequestHandler<GetUserQuery, GetUserResult>
{
    public async Task<GetUserResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userDbContext.Users.FindAsync(request.Id, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.Id.ToString()!);
        }

        return new GetUserResult(user.Id, user.FirstName, user.LastName);
    }
}