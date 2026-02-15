using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using LogisticsService.Domain.Enums;

namespace LogisticsService.Domain.Entities;

public class Loads : Entity
{
    public LoadStatus LoadStatus { get; set; }
    
    [Required]
    public CargoType CargoType { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Required]
    public DateTime PickupDate { get; set; }
    
    [Required]
    public int Weight { get; set; }
    
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
    public bool IsDeleted { get; set; } = false;
    
    [Required]
    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

}