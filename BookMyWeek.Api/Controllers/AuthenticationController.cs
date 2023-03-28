using System.Security.Claims;
using BookMyWeek.Application.Authentication;
using BookMyWeek.Application.Authentication.Model;
using BookMyWeek.Application.Authentication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = BookMyWeek.Application.Authentication.Interfaces.IAuthenticationService;

namespace BookMyWeek.Application.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
    {
        var user = await _authenticationService.Login(loginDto, cancellationToken);
        var claims = new List<Claim>
        {
            new (AuthenticationConstants.UserIdClaimType, user.UserId.ToString()),
            new (AuthenticationConstants.NameClaimType, user.Name),
            new (AuthenticationConstants.DescriptionClaimType, user.Description),
        };
        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
        return Ok(user.UserId);
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> Logout()
    {
        if (HttpContext.User.Identity is { IsAuthenticated: true })
        {
            HttpContext.Response.Cookies.Delete(AuthenticationConstants.CookieName);
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }

        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
    {
        await _authenticationService.Register(registerDto, cancellationToken);
        return Ok();
    }
}