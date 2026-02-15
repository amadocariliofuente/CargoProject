using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using IdentityService.Application.Mappers;
using IdentityService.Application.Models;
using IdentityService.Infrastructure.Interfaces;

namespace IdentityService.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<List<UsersModel>?> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        var userModels = UserMapper.EntityToModelList(users.ToList());
        
        return userModels;
    }
    
    public async Task<UsersModel?> GetUserById(string id)
    {
        var user = await _userRepository.GetUserById(id);
        var userModels = UserMapper.EntityToModel(user!);
        
        return userModels;
    }
    
    public async Task<UsersModel?> GetUserByEmail(string email)
    {
        var user = await _userRepository.GetUserByEmail(email);
        var userModels = UserMapper.EntityToModel(user!);
        
        return userModels;
    }

    public async Task<(bool Success, IEnumerable<string>? Errors)> RegisterAsync(RegisterUserDto dto)
    {
        return await _userRepository.RegisterAsync(dto);
    }

    
    public async Task<(bool Success, string? token, IEnumerable<string>? Errors)> LoginAsync(LoginUserDto dto)
    {
        return await _userRepository.LoginAsync(dto);
    }
}