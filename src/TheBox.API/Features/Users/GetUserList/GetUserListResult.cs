using TheBox.API.Features.Users.GetUser;

namespace TheBox.API.Features.Users.GetUserList;

public class GetUserListResult
{
    public GetUserListResult()
    {
    }

    public GetUserListResult(IEnumerable<GetUserResult> users)
    {
        Users.AddRange(users);
    }

    public List<GetUserResult> Users { get; set; } = new();
}
