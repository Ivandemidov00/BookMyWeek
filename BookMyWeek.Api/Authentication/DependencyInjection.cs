using BookMyWeek.Application.Authentication.Implementations;
using BookMyWeek.Application.Authentication.Interfaces;
using BookMyWeek.Application.Authentication.Middleware;
using BookMyWeek.Application.Authentication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;

namespace BookMyWeek.Application.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddCookieAuthentication(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<UserAccessor>()
            .AddScoped<IUserInitializer>(provider => provider.GetRequiredService<UserAccessor>())
            .AddScoped<IUserAccessor>(provider => provider.GetRequiredService<UserAccessor>());
        
        serviceCollection.AddScoped<UserAccessorMiddleware>();
        
        
             
        
        
        serviceCollection
            .AddSingleton<CustomCookieAuthenticationEvents>()
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => {
                options.Cookie.Name = AuthenticationConstants.CookieName;
                options.EventsType = typeof(CustomCookieAuthenticationEvents);
                options.Cookie.SameSite = SameSiteMode.None;
            });

        return serviceCollection;
    }

    public static IApplicationBuilder UseCookieAuthorization(this IApplicationBuilder builder)
    {
        builder.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.None,
            HttpOnly = HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always
        });
        
        builder
            .UseAuthentication()
            .UseAuthorization();

        builder.UseMiddleware<UserAccessorMiddleware>();

        return builder;
    }
}
