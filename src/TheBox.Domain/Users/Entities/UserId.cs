namespace TheBox.Domain.Users.Entities;

using TheBox.Domain.Shared.Entities;

public sealed class UserId : GuidValueObject
{
    public UserId(Guid value) : base(value)
    {
    }

    public UserId() : base(Guid.NewGuid())
    {
    }
}
