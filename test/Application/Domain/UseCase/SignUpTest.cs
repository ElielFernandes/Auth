using Auth.Domain.Repository;
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

        var login = new Login(userRepository);
        var response = login.Execute(inputLogin);

        Assert.Equal(name, response.Name);
    }
}