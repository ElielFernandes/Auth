using Auth.Domain.Entity;
using Auth.Domain.Repository;

namespace Auth.Domain.UseCase;

public class SignUp
{
    private readonly IUserRepository _userRepository;

    public SignUp(IUserRepository userRepository) 
        => _userRepository = userRepository;
    
    public void Execute(InputSignUp input)
    {
        var user = User.Create(input.Name, input.Email, input.Password);
        _userRepository.Save(user);
    }
}

public struct InputSignUp
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}