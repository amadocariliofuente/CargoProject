using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Application.DTOs;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SignInManager<Users> _signInManager;
    private readonly UserManager<Users> _userManager;
    
    public UserRepository(SignInManager<Users> signInManager, UserManager<Users> userManager)
    {
        this._signInManager = signInManager;
        this._userManager = userManager;
    }

    public async Task<IList<Users>> GetAllUsers()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<Users?> GetUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
    
    public async Task<Users?> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<(bool Success, IEnumerable<string>? Errors)> RegisterAsync(RegisterUserDto dto)
    {
        Users user = new()
        {
            UserName =  dto.Email,
            FirstName = dto.FirstName,
            SecondName = dto.SecondName,
            Age = dto.Age,
            Email = dto.Email,
            UserType = dto.UserType
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description);
            return (false, errors);
        }
        await _userManager.AddToRoleAsync(user, "User");
        
        return (true, Enumerable.Empty<string>());
    }      

    public async Task<(bool Success, string? token, IEnumerable<string>? Errors)> LoginAsync(LoginUserDto dto)
    {
        /*Users user = new Users()
        {
            Email = dto.Email,
        };
        var password = dto.Password;*/
        
        // Error handling
        IEnumerable<string> error = new string[] { "Invalid email or password" };
        var errorReturn = (false, string.Empty, error); 
        
        var currentUser = await _userManager.FindByEmailAsync(dto.Email);
        if (currentUser == null)
            return errorReturn;
        

        // Checking the password
        var result = await _signInManager.CheckPasswordSignInAsync(
            currentUser,
            dto.Password,
            lockoutOnFailure: true);

        if (!result.Succeeded)
            return errorReturn;
        
        // Generating Jwt token
        string? token = await this.GenerateJwtToken(currentUser);
        return (true, token, Enumerable.Empty<string>());
    }
    
    private async Task<string> GenerateJwtToken<TUser>(IdentityUser<TUser> user) where TUser : IEquatable<TUser>
    {
        var currentUser = await _userManager.FindByIdAsync(user.Id.ToString()!);
        var roles = await _userManager.GetRolesAsync(currentUser!);
        
        var claims  = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
            new Claim(ClaimTypes.Email, user.Email!)
        };
        
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("otJ5eltLpCgP7G7dIjtME2nymzjtZ5jGWgiqjqdLw90")); // Secret JWT Key

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "Cargo",
            audience: "Cargo",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}