using LogisticsService.Domain.Entities;

namespace LogisticsService.Infrastructure.Interfaces;

public interface ILoadsRepository
{
    Task<List<Loads>>  GetAllLoadsAsync(CancellationToken token);
    
    Task<Loads?> GetLoadsAsync(Guid loadId, CancellationToken token);
    
    Task<Loads> CreateLoadAsync(Loads loads, CancellationToken token);
    
    Task<Loads?> UpdateLoadAsync(Loads loads, CancellationToken token);
    
    Task<bool> DeleteLoadAsync(Guid loadId, Guid userId, CancellationToken token);
}