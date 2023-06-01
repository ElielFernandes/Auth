namespace Auth.Domain.Entity;

public class User
{
    public string Name { get; }
    public Email Email { get; }
    public Password Password { get; }

    private User(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public static User Create(string name, string email, string password)
    {
        return new User(name, Email.Create(email), Password.Create(password, "salt"));
    }
}