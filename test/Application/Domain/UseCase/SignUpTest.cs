using Auth.Domain.Repository;
using Auth.Domain.Service.Auth;
using Auth.Domain.UseCase;
using Auth.Infra.Repository.Memory;
using Xunit;

namespace Auth.test.Application.Domain.UseCase;

public class SignUpTest
{
    [Fact]
    public void TestSuccessfulSignUp()
    {
        const string name = "Eliel";
        const string email = "elielfernandes@test.com";
        const string password = "12345678";

        IUserRepository userRepository = new UserRepositoryMemory();
        var tokenGeneratorService = new TokenGenerator(Settings.Secret);
        
        var inputSignUp = new InputSignUp() {
            Name = name,
            Email = email,
            Password = password,
        };

        var signUp = new SignUp(userRepository);
        signUp.Execute(inputSignUp);

        var inputLogin = new InputLogin() {
            Email = email,
            Password = password,
        };

        var login = new Login(userRepository, tokenGeneratorService);
        var response = login.Execute(inputLogin);

        Assert.Equal(name, response.Name);
        Assert.Equal("Bearer", response.Type);
        Assert.Equal("7200", response.ExpireIn);
        Assert.Equal(email, tokenGeneratorService.Verify(response.Token));
    }

    [Fact]
    public void TestUnsuccessfulSignUp()
    {
        const string name = "Eliel";
        const string email = "elielfernandes@test.com";
        const string password = "12345678";

        IUserRepository userRepository = new UserRepositoryMemory();
        
        var inputSignUp = new InputSignUp() {
            Name = name,
            Email = email,
            Password = password,
        };
        
        var signUp = new SignUp(userRepository);
        signUp.Execute(inputSignUp);
        
        var exception = Assert.Throws<Exception>(() => signUp.Execute(inputSignUp));
        Assert.Equal("Sign up failed", exception.Message);
    }
}