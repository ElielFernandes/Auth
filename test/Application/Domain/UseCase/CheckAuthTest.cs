using Auth.Domain.Service.Auth;
using Auth.Domain.UseCase;
using Xunit;

namespace Auth.test.Application.Domain.UseCase;

public class CheckAuthTest
{
    [Fact]
    public void TestCheckAuth()
    {
        var tokenGeneratorService = new TokenGenerator(Settings.Secret);
        var checkAuth = new CheckAuth(tokenGeneratorService);
        const string input = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiRWxpZWwiLCJlbWFpbCI6ImVsaWVsZmVybmFuZGVzQHRlc3QuY29tIiwibmJmIjoxNjg1ODk0OTkxLCJleHAiOjE3NDU4OTQ5OTEsImlhdCI6MTY4NTg5NDk5MX0.H-yc9iEJ3fFXVScg9SVBCJmJS8BFDR1qXfHVU64bKeE";
        var response = checkAuth.Execute(input);
        
        Assert.Equal("elielfernandes@test.com", response.Email);
    }
    
    [Fact]
    public void TestFailedVerifyAuth()
    {
        var tokenGeneratorService = new TokenGenerator(Settings.Secret);
        var checkAuth = new CheckAuth(tokenGeneratorService);
        const string input = "";
        
        var exception = Assert.Throws<Exception>(() => checkAuth.Execute(input));
        Assert.Equal("Invalid token", exception.Message);
    }
}
