using BookMyWeek.Application.Authentication.Implementations;
using BookMyWeek.Application.Authentication.Interfaces;
using BookMyWeek.Application.Authentication.Models;
using Mapster;

namespace BookMyWeek.Application.Authentication.Middleware;

public class UserAccessorMiddleware : IMiddleware
{
    private readonly IUserInitializer _userInitializer;

    public UserAccessorMiddleware(IUserInitializer userInitializer)
        => _userInitializer = userInitializer;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var claims = context.User.Claims
                .Where(claim => claim.Type is
                    AuthenticationConstants.UserIdClaimType or
                    AuthenticationConstants.NameClaimType or
                    AuthenticationConstants.DescriptionClaimType)
                .ToDictionary(claim => claim.Type, claim => claim.Value);

            _userInitializer.UserId = Guid.Parse(claims[AuthenticationConstants.UserIdClaimType]);
            _userInitializer.Name = claims[AuthenticationConstants.NameClaimType];
            _userInitializer.Description = claims[AuthenticationConstants.DescriptionClaimType];
            
            await next(context);

        }
        else
        {
            await next(context);
        }
    }
}