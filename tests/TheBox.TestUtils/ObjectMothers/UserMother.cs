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

    public static List<User> CreateList(int count)
    {
        return Enumerable.Range(0, count).Select(i => CreateBase($"John {i}", $"Doe {i}")).ToList();
    }

    static User CreateBase(string firstName = "John", string lastName = "Doe")
    {
        return new User(firstName, lastName);
    }
}
