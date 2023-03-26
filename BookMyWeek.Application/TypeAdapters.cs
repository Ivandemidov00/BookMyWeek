using BookMyWeek.Application.User.Models;
using Mapster;

namespace BookMyWeek.Application;

public static class TypeAdapters
{
    public static void AddAdaptersType()
    {
        TypeAdapterConfig<Domain.UserDatabase, UserFindDto>
            .NewConfig()
            .IgnoreNullValues(true)
            .MapToConstructor(true);

    }
}