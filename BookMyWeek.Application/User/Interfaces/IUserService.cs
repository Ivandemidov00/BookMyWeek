using BookMyWeek.Application.User.Models;

namespace BookMyWeek.Application.User.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserFindDto>> GetAll(CancellationToken cancellationToken);
    
    
}