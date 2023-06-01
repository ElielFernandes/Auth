using Auth.Domain.Entity;
using Auth.Domain.Repository;

namespace Auth.Infra.Repository.Memory;

public class UserRepositoryMemory : IUserRepository
{
    private readonly List<User> _userMemory = new List<User>();
    
    public void Save(User user) 
    {
        _userMemory.Add(user);
    }

    public User? GetByEmail(string email) 
    {
        return _userMemory.FirstOrDefault(user => user.Email.Value == email);
    }
}