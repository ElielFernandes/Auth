using Auth.Domain.Entity;

namespace Auth.Domain.Service.Auth;

public interface ITokenGenerator
{
    public string Generate(User user, int expires = 7200, DateTime? issuedAt = null);
    public string? Verify(string? token);
}