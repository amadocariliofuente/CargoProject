using System.ComponentModel.DataAnnotations;
using LogisticsService.Domain.Enums;

namespace LogisticsService.Application.DTOs;

public class CreateLoadDto
{
    public LoadStatus LoadStatus { get; set; } = LoadStatus.Posted;
    
    [Required]
    public CargoType CargoType { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Required]
    public DateTime PickupDate { get; set; }

    [Required] 
    public int Weight { get; set; } = 100;
    
    // Delievery Properties
    
    [Required]
    public string DelieveryLocation { get; set; }
    
    [Required]
    public string DelieveryContact  { get; set; }
    
    [Required]
    public string? DelieveryInstructions { get; set; }
    
    [Required]
    public DateTime DeliveryDate { get; set; }
    
    public VehicleType VehicleType { get; set; }
    
    [Required]
    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }
}