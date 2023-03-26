using BookMyWeek.Application.Authentication.Interfaces;

namespace BookMyWeek.Application.Authentication.Implementations;

public class UserAccessor : IUserAccessor, IUserInitializer
{
    private Domain.User? _user;

    public Domain.User? User
    {
        set => _user = value;
    }

    public Guid UserId => _user!.UserId;
    
    public string Name => _user!.Name;
    
    public string Description => _user!.Description;
}