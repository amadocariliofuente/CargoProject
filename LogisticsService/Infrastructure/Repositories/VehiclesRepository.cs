using LogisticsService.Application.DTOs;
using LogisticsService.Domain.Entities;
using LogisticsService.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsService.Infrastructure.Repositories;

public class LoadsRepository(LogisticsDbContext logisticsContext) : ILoadsRepository
{
    private readonly LogisticsDbContext _logisticsContext = logisticsContext;
    
    public Task<List<Loads>> GetAllLoadsAsync(CancellationToken token)
    {
        return _logisticsContext.Loads.ToListAsync(token);
    }

    public async Task<Loads?> GetLoadsAsync(Guid loadId, CancellationToken token)
    {
        return await _logisticsContext.Loads.FirstOrDefaultAsync(l => l.Id == loadId, token);
    }

    public async Task<Loads> CreateLoadAsync(Loads loads, CancellationToken token)
    {
        await _logisticsContext.Loads.AddAsync(loads, token);
        await _logisticsContext.SaveChangesAsync(token);
        return loads;
    }

    public async Task<Loads?> UpdateLoadAsync(Loads load, CancellationToken token)
    {
        var currentLoad =  await _logisticsContext.Loads.FirstOrDefaultAsync(l => l.Id == load.Id, token);
        if (currentLoad != null)
        {
            currentLoad.LoadStatus = load.LoadStatus;
            currentLoad.CargoType = load.CargoType;
            currentLoad.Location = load.Location;
            currentLoad.PickupDate = load.PickupDate;
            currentLoad.Weight = load.Weight;
            currentLoad.DelieveryLocation = load.DelieveryLocation;
            currentLoad.DelieveryContact = load.DelieveryContact;
            currentLoad.DelieveryInstructions = load.DelieveryInstructions;
            currentLoad.DeliveryDate = load.DeliveryDate;
            currentLoad.VehicleType = load.VehicleType;
            
            await _logisticsContext.SaveChangesAsync(token);
        }
        return currentLoad;
    }

    public async Task<bool> DeleteLoadAsync(Guid loadId, Guid userId, CancellationToken token)
    {
        var load = await _logisticsContext.Loads.FirstOrDefaultAsync(u => u.Id == loadId, token);
        
        if (load == null || load.CreatedByUserId != userId)
        {
            return false;
        }

        load.IsDeleted = true;
        return true;
    }
}