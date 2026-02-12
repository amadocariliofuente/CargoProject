using IdentityService.Application.DTOs;

namespace IdentityService.Application.Interfaces;

public interface IUserService
{
    Task<(bool Success, RegisterUserDto? User, IEnumerable<string>? Errors)> RegisterAsync(RegisterUserDto dto);
    Task<string?> LoginAsync(LoginDto dto);
}