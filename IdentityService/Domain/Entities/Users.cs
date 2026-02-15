using System.ComponentModel.DataAnnotations;
using IdentityService.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain.Entities;

public class Users : IdentityUser<Guid> // IdentityUser already has UserName, Email and Password properties
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string SecondName { get; set; }
    
    [Required]
    [Range(13, 120)]
    public int Age { get; set; }
    
    public UserType UserType { get; set; }

    public DateTime CreatedDate { get; set; }
}