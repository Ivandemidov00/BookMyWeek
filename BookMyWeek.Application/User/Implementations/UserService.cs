using BookMyWeek.Application.User.Interfaces;
using BookMyWeek.Application.User.Models;
using BookMyWeek.Infrastructure.User.Interfaces;
using Mapster;

namespace BookMyWeek.Application.User.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserFindDto>> GetAll(CancellationToken cancellationToken)
        => (await _userRepository.GetAll(cancellationToken)).Adapt<List<UserFindDto>>();
}