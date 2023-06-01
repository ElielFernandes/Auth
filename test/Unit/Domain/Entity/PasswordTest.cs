using Auth.Domain.Entity;
using Xunit;

namespace Auth.test.Unit.Domain.Entity;

public class PasswordTest
{
    [Fact]
    public void TestSuccessfulCreatePassword()
    {
        var password = Password.Create("12345678", "salt");
        Assert.Equal("F1FA680348802C16E610E0AFA109EF9FD2EA2100", password.Value);
        Assert.Equal("salt", password.Salt);
        Assert.True(password.IsValid("12345678"));
    }
    
    [Fact]
    public void TestUnsuccessfulCreatePassword()
    {
        var exception = Assert.Throws<Exception>(() => Password.Create("1234567", "salt"));
        Assert.Equal("Invalid password", exception.Message);
    }
}