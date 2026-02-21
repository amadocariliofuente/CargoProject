using LogisticsService.Application.DTOs;
using LogisticsService.Domain.Entities;
using LogisticsService.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsService.Infrastructure.Repositories;

public class VehiclesRepository(LogisticsDbContext logisticsContext) : IVehiclesRepository
{
    private readonly LogisticsDbContext _logisticsContext = logisticsContext;
    
    public Task<List<Vehicles>> GetAllVehiclesAsync(CancellationToken token)
    {
        return _logisticsContext.Vehicles
            .AsNoTracking()
            .Where(l => l.IsDeleted == false)
            .ToListAsync(token);
    }

    public async Task<Vehicles?> GetVehiclesAsync(Guid loadId, CancellationToken token)
    {
        return await _logisticsContext.Vehicles
            .AsNoTracking()
            .Where(l => l.IsDeleted == false)
            .FirstOrDefaultAsync(l => l.Id == loadId, token);
    }

    public async Task<Vehicles> CreateVehiclesAsync(Vehicles vehicles, CancellationToken token)
    {
        await _logisticsContext.Vehicles.AddAsync(vehicles, token);
        await _logisticsContext.SaveChangesAsync(token);
        return vehicles;
    }

    public async Task<Vehicles?> UpdateVehiclesAsync(Vehicles vehicles, CancellationToken token)
    {
        var currentVehicle =  await _logisticsContext.Vehicles.FirstOrDefaultAsync(l => l.Id == vehicles.Id, token);
        if (currentVehicle != null)
        {
            currentVehicle.VehiclePlate = vehicles.VehiclePlate;
            currentVehicle.VehicleLocation = vehicles.VehicleLocation;
            currentVehicle.VehicleSize = vehicles.VehicleSize;
            currentVehicle.VehicleType = vehicles.VehicleType;

            await _logisticsContext.SaveChangesAsync(token);
        }
        return currentVehicle;
    }

    public async Task<bool> DeleteVehiclesAsync(Guid vehicleId, Guid userId, CancellationToken token)
    {
        var vehicle = await _logisticsContext.Vehicles.FirstOrDefaultAsync(u => u.Id == vehicleId, token);
        
        if (vehicle == null || vehicle.OwnerUserId != userId)
        {
            return false;
        }

        vehicle.IsDeleted = true;
        return true;
    }
}