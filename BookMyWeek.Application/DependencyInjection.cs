using BookMyWeek.Application.Authentication.Implementations;
using BookMyWeek.Application.Authentication.Interfaces;
using BookMyWeek.Application.Range.Implementations;
using BookMyWeek.Application.Range.Interfaces;
using BookMyWeek.Application.User.Implementations;
using BookMyWeek.Application.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookMyWeek.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        TypeAdapters.AddAdaptersType();
        serviceCollection
            .AddSingleton<IUserService, UserService>()
            .AddSingleton<IRangeService, RangeService>()
            .AddSingleton<IAuthenticationService, AuthenticationService>();
        return serviceCollection;
    }
}