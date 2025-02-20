#region
using TheBox.Domain.Users.Entities;
#endregion

namespace TheBox.TestUtils.ObjectMothers;

public static class UserMother
{
    public static User Create()
    {
        return CreateBase();
    }

    static User CreateBase()
    {
        return new User("John", "Doe");
    }
}
