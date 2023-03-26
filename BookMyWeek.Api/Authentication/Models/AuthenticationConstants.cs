namespace BookMyWeek.Application.Authentication.Models;

internal static class AuthenticationConstants
{
    public const string CookieName = "BookMyWeek.Authentication";
    
    public const string UserIdClaimType = nameof(Domain.User.UserId);

    public const string NameClaimType = nameof(Domain.User.Name);

    public const string DescriptionClaimType = nameof(Domain.User.Description);
}