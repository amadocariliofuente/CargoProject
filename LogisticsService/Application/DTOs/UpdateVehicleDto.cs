using System.ComponentModel.DataAnnotations;
using LogisticsService.Domain.Enums;

namespace LogisticsService.Application.DTOs;

public class UpdateVehicleDto
{
    [Required]
    public VehicleType VehicleType { get; set; }
    
    [Required]
    public string VehiclePlate { get; set; }
    
    [Required]
    public string VehicleLocation { get; set; }
    
    [Required]
    public string VehicleSize { get; set; }
    
}