using BookMyWeek.Application.Authentication.Interfaces;

namespace BookMyWeek.Application.Authentication.Implementations;

public class UserAccessor : IUserAccessor, IUserInitializer
{
    public Guid UserId { get; set; }

    public string Name { get; set; }
    
    public string Description { get; set; }
}