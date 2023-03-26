using BookMyWeek.Infrastructure.AllowedRange.Implementations;
using BookMyWeek.Infrastructure.AllowedRange.Interfaces;
using BookMyWeek.Infrastructure.ConnectionFactory.Interfaces;
using BookMyWeek.Infrastructure.Range.Implementations;
using BookMyWeek.Infrastructure.Range.Interfaces;
using BookMyWeek.Infrastructure.User.Implementations;
using BookMyWeek.Infrastructure.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookMyWeek.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IConnectionFactory, ConnectionFactory.Implementations.ConnectionFactory>()
            .AddSingleton<IUserRepository, UserRepository>()
            .AddSingleton<IRangeRepository, RangeRepository>()
            .AddSingleton<IAllowedRangeRepository, AllowedRangeRepository>();
        
        return serviceCollection;
    }
}