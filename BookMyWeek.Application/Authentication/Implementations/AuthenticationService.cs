using BookMyWeek.Application.Authentication.Interfaces;
using BookMyWeek.Infrastructure.User.Interfaces;

namespace BookMyWeek.Application.Authentication.Implementations;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
        => _userRepository = userRepository;
    
    public async Task<Domain.UserDatabase> Login(LoginDto loginDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsername(loginDto.UserName, cancellationToken);
        if (!VerifyPassword(loginDto.Password, user.Hash, user.Salt))
        {
            throw new Exception("");
        }
        return user;
    }

    public async Task Register(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        CreatePasswordHash(registerDto.Password, out var hash, out var salt);
        await _userRepository.Add(new Domain.UserDatabase(Guid.NewGuid(), registerDto.UserName, registerDto.Description, hash, salt), cancellationToken);
    }
    
    private static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
    }
    
    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
}