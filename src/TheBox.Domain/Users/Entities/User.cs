namespace TheBox.Domain.Users.Entities;

public sealed class User
{
    public UserId Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public User(string firstName, string lastName)
    {
        AssertIsValidFirstName(firstName);
        AssertIsValidLastName(lastName);

        Id = new UserId();
        FirstName = firstName;
        LastName = lastName;
    }

    public void SetFirstName(string firstName, string lastName)
    {
        AssertIsValidFirstName(firstName);
        FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        AssertIsValidLastName(lastName);
        LastName = lastName;
    }

    private static void AssertIsValidFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("FirstName cannot be empty.");
        }
    }
    
    private static void AssertIsValidLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("LastName cannot be empty.");
        }
    }
}