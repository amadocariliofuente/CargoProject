using IdentityService.Application.DTOs;
using IdentityService.Domain.Entities;

namespace IdentityService.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<(bool Success, IEnumerable<string>? Errors)> RegisterAsync(RegisterUserDto dto);
    Task<(bool Success, string? token, IEnumerable<string>? Errors)> LoginAsync(LoginUserDto dto);
    Task<IList<Users>> GetAllUsers();
    Task<Users?> GetUserById(string id);
    Task<Users?> GetUserByEmail(string email);
}