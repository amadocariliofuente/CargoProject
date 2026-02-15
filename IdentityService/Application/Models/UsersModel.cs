using System.ComponentModel.DataAnnotations;
using IdentityService.Domain.Enums;

namespace IdentityService.Application.Models;

public class UsersModel
{
    public string FirstName { get; set; }
    
    public string SecondName { get; set; }
    
    public string Email { get; set; }
    
    public int Age { get; set; }
    
    public UserType UserType { get; set; }

    public DateTime CreatedDate { get; set; }
}