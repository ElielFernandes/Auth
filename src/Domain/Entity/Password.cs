using System.Security.Cryptography;
using System.Text;

namespace Auth.Domain.Entity;

public class Password
{
    public string Value { get; }
    public string Salt { get; }

    private Password(string value, string salt)
    {
        Value = value;
        Salt = salt;
    }
    
    public static Password Create(string value, string? salt)
    {
        if (value.Length < 8)
            throw new Exception("Invalid password");
        
        var generatedSalt = salt is not null 
            ? Encoding.UTF8.GetBytes(salt) 
            : RandomNumberGenerator.GetBytes(64);
        
        var password = HashPassword(value, generatedSalt, 100, 20);
        
        return new Password(password, Encoding.UTF8.GetString(generatedSalt));
    }
    
    public bool IsValid(string plainPassword)
    {
        var password = HashPassword(plainPassword, Encoding.UTF8.GetBytes(Salt) , 100, 20);
        return password.Equals(Value);
    }
    
    private static string HashPassword(string password, byte[] salt, int iterations, int size)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA512,
            size);
        return Convert.ToHexString(hash);
    }
}