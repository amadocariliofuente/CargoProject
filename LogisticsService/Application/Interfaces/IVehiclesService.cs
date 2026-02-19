using LogisticsService.Application.DTOs;

namespace LogisticsService.Application.Interfaces;

public interface ILoadsService
{
    Task<List<LoadDto>>  GetAllLoadsAsync(CancellationToken token);
    
    Task<LoadDto?> GetLoadsAsync(Guid loadId, CancellationToken token);
    
    Task<(LoadDto, bool result)> CreateLoadAsync(CreateLoadDto loads, CancellationToken token);
    
    Task<LoadDto?> UpdateLoadAsync(LoadDto loads, CancellationToken token);
    
    Task<bool> DeleteLoadAsync(Guid loadId, Guid userId, CancellationToken token);
}