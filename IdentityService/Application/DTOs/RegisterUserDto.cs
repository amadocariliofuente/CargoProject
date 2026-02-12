using System.ComponentModel.DataAnnotations;
using IdentityService.Domain.Enums;

namespace IdentityService.Application.DTOs;

public class RegisterUserDto
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required] public string SecondName { get; set; } = null!;

    [Required] public string Email { get; set; } = null!;
    
    [Required]
    public string Password { get; set; } = null!;
    
    [Required]
    [Range(13, 120)]
    public int Age { get; set; }
    
    public UserType UserType { get; set; }
}