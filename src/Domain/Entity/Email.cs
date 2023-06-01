namespace Auth.Domain.Entity;
public partial class Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (!MyRegex().IsMatch(value)) {
            throw new Exception("Invalid email");
        }

        return new Email(value);
    }

    [System.Text.RegularExpressions.GeneratedRegex("^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov)$")]
    private static partial System.Text.RegularExpressions.Regex MyRegex();
}