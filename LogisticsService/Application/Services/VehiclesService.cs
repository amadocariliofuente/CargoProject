using LogisticsService.Application.DTOs;
using LogisticsService.Application.Interfaces;
using LogisticsService.Application.Mappers;
using LogisticsService.Infrastructure.Interfaces;
using Shared.Contracts;

namespace LogisticsService.Application.Services;

public class LoadsService(ILoadsRepository loadsRepository, IdentityService.IdentityServiceClient identityClient) : ILoadsService
{
    private readonly ILoadsRepository _loadsRepository = loadsRepository;
    private readonly IdentityService.IdentityServiceClient _identityClient = identityClient;
    
    public async Task<List<LoadDto>> GetAllLoadsAsync(CancellationToken token)
    {
        var loads = await _loadsRepository.GetAllLoadsAsync(token);
        var loadDtos = LoadMappers.EntityToDtoList(loads);
        
        return loadDtos;
    }

    public async Task<LoadDto?> GetLoadsAsync(Guid loadId, CancellationToken token)
    {
        var load = await _loadsRepository.GetLoadsAsync(loadId, token);
        var loadDto = LoadMappers.EntityToDto(load);
        
        // Getting owner user's email
        var userMail = await GetUserEmailAsync(loadDto.CreatedByUserId);
        loadDto.CreatedbyUserEmail = userMail;
        
        return loadDto;
    }

    public async Task<(LoadDto, bool result)> CreateLoadAsync(CreateLoadDto loads, CancellationToken token)
    {
        var load = LoadMappers.CreateDtoToEntity(loads);
        var result = await _loadsRepository.CreateLoadAsync(load, token);
        var loadDto = LoadMappers.EntityToDto(result);
        
        // Some validation logic here, should be written later  ... 
        
        return (loadDto, true);
    }

    public async Task<LoadDto?> UpdateLoadAsync(LoadDto loads, CancellationToken token)
    {
        var load = LoadMappers.DtoToEntity(loads);
        var result = await _loadsRepository.UpdateLoadAsync(load, token);
        var loadDto = LoadMappers.EntityToDto(result);
        
        return loadDto;
    }

    public async Task<bool> DeleteLoadAsync(Guid loadId, Guid userId,CancellationToken token)
    {
        var result = await _loadsRepository.DeleteLoadAsync(loadId, userId, token);
        return result;
    }
    
    
    private async Task<string> GetUserEmailAsync(Guid userId)
    {
        string userIdStr = userId.ToString();
        
        var response = await _identityClient.GetUserEmailAsync(
            new GetUserEmailRequest { UserId = userIdStr });

        return response.Email;
    }
}