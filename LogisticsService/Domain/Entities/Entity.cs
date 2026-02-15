using System.ComponentModel.DataAnnotations;

namespace LogisticsService.Domain.Entities;

public abstract class Entity
{
    [Key]
    public Guid Id { get; set; }
}