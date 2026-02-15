using IdentityService.Application.DTOs;
using IdentityService.Application.Models;

namespace IdentityService.Application.Interfaces;

public interface IUserService
{
    Task<(bool Success, IEnumerable<string>? Errors)> RegisterAsync(RegisterUserDto dto);
    Task<(bool Success, string? token, IEnumerable<string>? Errors)> LoginAsync(LoginUserDto userDto);
    Task<UsersModel?> GetUserByEmail(string email);
    Task<UsersModel?> GetUserById(string id);
    Task<List<UsersModel>?> GetAllUsers();
}