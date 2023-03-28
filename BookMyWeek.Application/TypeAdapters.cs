using BookMyWeek.Application.User.Models;
using BookMyWeek.Domain;
using BookMyWeek.Infrastructure.AllowedRange.Models;
using BookMyWeek.Infrastructure.Range.Models;
using Mapster;

namespace BookMyWeek.Application;

public static class TypeAdapters
{
    public static void AddAdaptersType()
    {
        TypeAdapterConfig<UserDatabase, UserFindDto>
            .NewConfig()
            .IgnoreNullValues(true)
            .MapToConstructor(true);

        TypeAdapterConfig<AllowedRangeDatabase, AllowRange>
            .NewConfig()
            .IgnoreNullValues(true)
            .MapToConstructor(true);
        
        TypeAdapterConfig<EventRangeDatabase, EventRange>
            .NewConfig()
            .IgnoreNullValues(true)
            .MapToConstructor(true);
    }
}