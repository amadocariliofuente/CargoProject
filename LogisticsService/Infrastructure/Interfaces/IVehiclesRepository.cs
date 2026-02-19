using LogisticsService.Domain.Entities;

namespace LogisticsService.Infrastructure.Interfaces;

public interface IVehiclesRepository
{
    Task<List<Vehicles>>  GetAllVehiclesAsync(CancellationToken token);
    
    Task<Vehicles?> GetVehiclesAsync(Guid vehicleId, CancellationToken token);
    
    Task<Vehicles> CreateVehiclesAsync(Vehicles vehicles, CancellationToken token);
    
    Task<Vehicles?> UpdateVehiclesAsync(Vehicles vehicles, CancellationToken token);
    
    Task<bool> DeleteVehiclesAsync(Guid vehicleId, Guid userId, CancellationToken token);
}