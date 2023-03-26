using BookMyWeek.Application.User.Interfaces;
using BookMyWeek.Application.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookMyWeek.Application.Controllers;

[ApiController]
[Route("api/userDatabase")]
public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("all")]
    public Task<IEnumerable<UserFindDto>> GetAll(CancellationToken cancellationToken)
        => _userService.GetAll(cancellationToken);
}