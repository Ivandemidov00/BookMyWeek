namespace BookMyWeek.Application.Authentication.Interfaces;

public interface IUserInitializer
{
    public Guid UserId { set; }

    public string Name { set; }
    
    public string Description { set; }
}