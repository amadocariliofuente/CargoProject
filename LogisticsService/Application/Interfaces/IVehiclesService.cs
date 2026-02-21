using LogisticsService.Application.DTOs;

namespace LogisticsService.Application.Interfaces;

public interface IVehiclesService
{
    Task<List<VehicleResponseDto>>  GetAllVehiclesAsync(CancellationToken token);
    
    Task<VehicleResponseDto?> GetVehiclesAsync(Guid vehicleId, CancellationToken token);
    
    Task<(VehicleResponseDto, bool result)> CreateVehicleAsync(CreateVehicleDto vehicles, Guid ownerUserId, CancellationToken token);
    
    Task<VehicleResponseDto?> UpdateVehicleAsync(UpdateVehicleDto vehiclesRequest, Guid vehicleId, CancellationToken token);
    
    Task<bool> DeleteVehicleAsync(Guid vehicleId, Guid userId, CancellationToken token);
}