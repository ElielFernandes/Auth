using Auth.Domain.Entity;
using Xunit;

namespace Auth.test.Unit.Domain.Entity;

public class EmailTest
{
    [Fact]
    public void TestSuccessfulCreateEmail()
    {
        var email = Email.Create("elielfernandes@test.com");
        Assert.Equal("elielfernandes@test.com", email.Value);
    }
    
    [Fact]
    public void TestUnsuccessfulCreateEmail()
    {
        var exception = Assert.Throws<Exception>(() => Email.Create("elielfernandestest.com"));
        Assert.Equal("Invalid email", exception.Message);
    }
}