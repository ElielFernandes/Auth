using Auth.Domain.Repository;
using Auth.Domain.Service.Auth;

namespace Auth.Domain.UseCase;

public class Login
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;

    public Login(IUserRepository userRepository, ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }
    
    public LoginResponse Execute(InputLogin input)
    {
        var user = _userRepository.GetByEmail(input.Email);

        if (user is null) {
            throw new Exception("Authentication failed");
        }

        if (!user.Password.IsValid(input.Password)) {
            throw new Exception("Authentication failed");
        }
        
        var tokenGenerator = _tokenGenerator.Generate(user);

        return new LoginResponse() {
            Name = user.Name,
            Token = tokenGenerator,
            Type = "Bearer",
            ExpireIn = "7200",
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