namespace BookMyWeek.Application.Authentication.Interfaces;

public interface IUserInitializer
{
    Domain.User? User { set; }
}