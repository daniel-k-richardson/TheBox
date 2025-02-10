using TheBox.Domain.Shared.Entities;

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