using System.ComponentModel.DataAnnotations;
using LogisticsService.Domain.Enums;

namespace LogisticsService.Application.DTOs;

public class VehicleRequestDto
{
    [Required]
    public Guid Id { get; set; }

    /*[Required]
    public Guid OwnerUserId  { get; set; }*/
    
    [Required]
    public string OwnerUserEmail { get; set; }
    
    [Required]
    public VehicleType VehicleType { get; set; }
    
    [Required]
    public string VehiclePlate { get; set; }
    
    [Required]
    public string VehicleLocation { get; set; }
    
    [Required]
    public string VehicleSize { get; set; }
    
    [Required]
    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedDate { get; set; }
    
}