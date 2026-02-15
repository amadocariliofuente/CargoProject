using IdentityService.Application;
using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto registerUsersDto)
    {
        var result = await _userService.RegisterAsync(registerUsersDto);
        if (!result.Success)
            return BadRequest(result.Errors);

        return Ok(result.Success); // RegisterUserDto returned
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto loginUsersDto)
    {
        var result = await _userService.LoginAsync(loginUsersDto);
        if (!result.Success)
            return BadRequest(result.Errors);

        return Ok(new { success = result.Success , token = result.token });
    }
}