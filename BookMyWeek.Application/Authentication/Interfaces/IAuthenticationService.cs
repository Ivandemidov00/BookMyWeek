namespace BookMyWeek.Application.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<Domain.UserDatabase> Login(LoginDto loginDto, CancellationToken cancellationToken);

    Task Register(RegisterDto registerDto, CancellationToken cancellationToken);
}