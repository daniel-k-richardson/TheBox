#region
using TheBox.Domain.Shared.Entities;
#endregion

namespace TheBox.Domain.Users.Entities;

public sealed class UserId : GuidValueObject
{
    public UserId(Guid value) : base(value)
    {
    }

    public UserId() : base(Guid.NewGuid())
    {
    }
}
