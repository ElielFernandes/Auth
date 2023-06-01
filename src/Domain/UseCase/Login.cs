using Auth.Domain.Repository;

namespace Auth.Domain.UseCase;

public class Login
{
    private readonly IUserRepository _userRepository;

    public Login(IUserRepository userRepository) 
        => _userRepository = userRepository;
    
    public LoginResponse Execute(InputLogin input)
    {
        var user = _userRepository.GetByEmail(input.Email);

        if (user is null) {
            throw new Exception("Authentication failed");
        }

        if (!user.Password.IsValid(input.Password)) {
            throw new Exception("Authentication failed");
        }

        return new LoginResponse() {
            Name = user.Name
        };
    }
}

public struct InputLogin
{
    public string Email { get; init; }
    public string Password { get; init; }
}

public struct LoginResponse
{
    public string Name { get; set; }
    public string Token { get; set; }
    public string Type { get; set; }
    public string ExpireIn { get; set; }
}