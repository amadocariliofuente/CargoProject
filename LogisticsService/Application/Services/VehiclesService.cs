using LogisticsService.Application.DTOs;
using LogisticsService.Application.Interfaces;
using LogisticsService.Application.Mappers;
using LogisticsService.Infrastructure.Interfaces;
using Shared.Contracts;

namespace LogisticsService.Application.Services;

public class VehiclesService(IVehiclesRepository vehiclesRepository, IdentityService.IdentityServiceClient identityClient) : IVehiclesService
{
    private readonly IVehiclesRepository _vehiclesRepository = vehiclesRepository;
    private readonly IdentityService.IdentityServiceClient _identityClient = identityClient;
    
    public async Task<List<VehicleResponseDto>> GetAllVehiclesAsync(CancellationToken token)
    {
        var vehicles = await _vehiclesRepository.GetAllVehiclesAsync(token);
        var vehiclesDtos = VehicleMappers.EntityToResponseDtoList(vehicles);
        
        return vehiclesDtos;
    }

    public async Task<VehicleResponseDto?> GetVehiclesAsync(Guid vehicleId, CancellationToken token)
    {
        var vehicle = await _vehiclesRepository.GetVehiclesAsync(vehicleId, token);

        if (vehicle.IsDeleted)
        {
            return null;
        }
        var vehicleDto = VehicleMappers.EntityToResponseDto(vehicle);
        
        // Getting owner user's email
        var userMail = await GetUserEmailAsync(vehicleDto.OwnerUserId);
        vehicleDto.OwnerUserEmail = userMail;
        
        return vehicleDto;
    }

    public async Task<(VehicleResponseDto, bool result)> CreateVehicleAsync(CreateVehicleDto vehicles, Guid ownerUserId, CancellationToken token)
    {
        var vehicle = VehicleMappers.CreateDtoToEntity(vehicles, ownerUserId);
        var result = await _vehiclesRepository.CreateVehiclesAsync(vehicle, token);
        var vehicleDto = VehicleMappers.EntityToResponseDto(result);
        
        // Some validation logic here, should be written later  ... 
        
        return (vehicleDto, true);
    }

    public async Task<VehicleResponseDto?> UpdateVehicleAsync(UpdateVehicleDto vehiclesRequest, CancellationToken token)
    {
        var vehicle = VehicleMappers.UpdateDtoToEntity(vehiclesRequest);
        var result = await _vehiclesRepository.UpdateVehiclesAsync(vehicle, token);
        var vehicleDto = VehicleMappers.EntityToResponseDto(result);
        
        return vehicleDto;
    }

    public async Task<bool> DeleteVehicleAsync(Guid vehicleId, Guid userId,CancellationToken token)
    {
        var result = await _vehiclesRepository.DeleteVehiclesAsync(vehicleId, userId, token);
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