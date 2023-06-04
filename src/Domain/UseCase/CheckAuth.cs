using Auth.Domain.Service.Auth;

namespace Auth.Domain.UseCase;

public class CheckAuth
{
    private readonly ITokenGenerator _tokenGenerator;

    public CheckAuth (ITokenGenerator tokenGenerator) 
        => _tokenGenerator = tokenGenerator;
    
    public CheckAuthResponse Execute(string token)
    {
        var email = _tokenGenerator.Verify(token);

        if (email is null) {
            throw new Exception("Invalid token");
        }
        
        return new CheckAuthResponse() {
            Email = email
        };
    }
}

public struct CheckAuthResponse
{
    public string Email { get; init; }
}