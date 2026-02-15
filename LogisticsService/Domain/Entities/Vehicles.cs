using System.ComponentModel.DataAnnotations;
using LogisticsService.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsService.Domain.Entities;

public class Vehicles : Entity
{
    [Required]
    public Guid OwnerUserId  { get; set; }
    
    [Required]
    public VehicleType VehicleType { get; set; }
    
    [Required]
    public string VehiclePlate { get; set; }
    
    [Required]
    public string VehicleLocation { get; set; }
    
    [Required]
    public string VehicleSize { get; set; }
}