using Auth.Domain.Entity;

namespace Auth.Domain.Repository;

public interface IUserRepository
{
    public void Save(User user);
    
    public User? GetByEmail(string email);
}