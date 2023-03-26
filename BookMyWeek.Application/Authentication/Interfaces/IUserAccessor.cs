namespace BookMyWeek.Application.Authentication.Interfaces;

public interface IUserAccessor
{
    public Guid UserId { get; }

    public string Name { get; }

    public string Description { get; }
    
}