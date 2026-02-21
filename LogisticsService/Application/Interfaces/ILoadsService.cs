using LogisticsService.Application.DTOs;

namespace LogisticsService.Application.Interfaces;

public interface ILoadsService
{
    Task<List<LoadResponseDto>>  GetAllLoadsAsync(CancellationToken token);
    
    Task<LoadResponseDto?> GetLoadAsync(Guid loadId, CancellationToken token);
    
    Task<(LoadResponseDto, bool result)> CreateLoadAsync(CreateLoadDto loads, Guid createdByUserId, CancellationToken token);
    
    Task<LoadResponseDto?> UpdateLoadAsync(UpdateLoadDto loadsResponse, Guid loadId, CancellationToken token);
    
    Task<bool> DeleteLoadAsync(Guid loadId, Guid userId, CancellationToken token);
}