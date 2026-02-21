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
    
    public async Task<List<LoadResponseDto>> GetAllLoadsAsync(CancellationToken token)
    {
        var loads = await _loadsRepository.GetAllLoadsAsync(token);
        if (loads.Any())
            return new List<LoadResponseDto>();
        
        var loadDtos = LoadMappers.EntityToResponseDtoList(loads);

        foreach (var loadDto in loadDtos)
        {
            // Getting owner user's email
            var userMail = await GetUserEmailAsync(loadDto.CreatedByUserId);
            loadDto.CreatedbyUserEmail = userMail;
        }
        
        return loadDtos;
    }

    public async Task<LoadResponseDto?> GetLoadAsync(Guid loadId, CancellationToken token)
    {
        var load = await _loadsRepository.GetLoadsAsync(loadId, token);
        if (load is null)
        {
            return null;
        }
        
        var loadDto = LoadMappers.EntityToResponseDto(load);
        
        // Getting owner user's email
        var userMail = await GetUserEmailAsync(loadDto.CreatedByUserId);
        loadDto.CreatedbyUserEmail = userMail;
        
        return loadDto;
    }

    public async Task<(LoadResponseDto, bool result)> CreateLoadAsync(CreateLoadDto loads, Guid createdByUserId, CancellationToken token)
    {
        var load = LoadMappers.CreateDtoToEntity(loads, createdByUserId);
        var result = await _loadsRepository.CreateLoadAsync(load, token);
        var loadDto = LoadMappers.EntityToResponseDto(result);
        
        // Getting owner user's email
        var userMail = await GetUserEmailAsync(loadDto.CreatedByUserId);
        loadDto.CreatedbyUserEmail = userMail;
        
        return (loadDto, true);
    }

    public async Task<LoadResponseDto?> UpdateLoadAsync(UpdateLoadDto loadsResponse, Guid loadId, CancellationToken token)
    {
        var load = LoadMappers.UpdateDtoToEntity(loadsResponse, loadId);
        var result = await _loadsRepository.UpdateLoadAsync(load, token);
        var loadDto = LoadMappers.EntityToResponseDto(result);
        
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