using System.Collections.Immutable;
using LogisticsService.Application.DTOs;
using LogisticsService.Domain.Entities;

namespace LogisticsService.Application.Mappers;

public class VehicleMappers
{
    #region EntityToResponseDto
    public static List<VehicleResponseDto> EntityToResponseDtoList(List<Vehicles> vehicles)
    {
        var result = new List<VehicleResponseDto>();
        foreach (var vehicle in vehicles)
        {
            var vehicleDto = new VehicleResponseDto()
            {
                Id = vehicle.Id,
                OwnerUserId = vehicle.OwnerUserId,
                VehiclePlate = vehicle.VehiclePlate,
                VehicleLocation =  vehicle.VehicleLocation,
                VehicleSize =  vehicle.VehicleSize,
                VehicleType = vehicle.VehicleType,
                IsDeleted = vehicle.IsDeleted,
                CreatedDate = vehicle.CreatedDate
            };
            result.Add(vehicleDto);
        }
        
        return result;
    }
    
    public static VehicleResponseDto EntityToResponseDto(Vehicles? vehicle)
    {
        var vehicleDto = new VehicleResponseDto()
        {
            Id = vehicle.Id,
            OwnerUserId = vehicle.OwnerUserId,
            VehiclePlate = vehicle.VehiclePlate,
            VehicleLocation =  vehicle.VehicleLocation,
            VehicleSize =  vehicle.VehicleSize,
            VehicleType = vehicle.VehicleType,
            IsDeleted = vehicle.IsDeleted,
            CreatedDate = vehicle.CreatedDate
        };
        return vehicleDto;
    }
    
    #endregion

    #region RequestDtoToEntity
    public static List<Vehicles> RequestDtoToEntityList(List<VehicleRequestDto> vehicleDtos)
    {
        var result = new List<Vehicles>();
        foreach (var vehicleDto in vehicleDtos)
        {
            var vehicle = new Vehicles()
            {
                Id = vehicleDto.Id,
                VehiclePlate = vehicleDto.VehiclePlate,
                VehicleLocation =  vehicleDto.VehicleLocation,
                VehicleSize =  vehicleDto.VehicleSize,
                VehicleType = vehicleDto.VehicleType,
            };
            result.Add(vehicle);
        }
        
        return result;
    }
    
    public static Vehicles RequestDtoToEntity(VehicleRequestDto vehicleRequestDto)
    {
        var vehicle = new Vehicles()
        {
            Id = vehicleRequestDto.Id,
            VehiclePlate = vehicleRequestDto.VehiclePlate,
            VehicleLocation =  vehicleRequestDto.VehicleLocation,
            VehicleSize =  vehicleRequestDto.VehicleSize,
            VehicleType = vehicleRequestDto.VehicleType,
        };

        return vehicle;
    }
    
    #endregion
    
    #region CreateDtoToEntity
    public static Vehicles CreateDtoToEntity(CreateVehicleDto? createVehicleDto, Guid ownerUserId)
    {
        var vehicle = new Vehicles()
        {
            Id = createVehicleDto.Id,
            VehiclePlate = createVehicleDto.VehiclePlate,
            VehicleLocation = createVehicleDto.VehicleLocation,
            VehicleSize = createVehicleDto.VehicleSize,
            VehicleType = createVehicleDto.VehicleType,
            OwnerUserId =  ownerUserId,
            CreatedDate = DateTime.Now.ToUniversalTime()
        };
        return vehicle;
    }
    #endregion
    
    #region UpdateDtoToEntity
    public static Vehicles UpdateDtoToEntity(UpdateVehicleDto? updateVehicleDto)
    {
        var vehicle = new Vehicles()
        {
            Id = updateVehicleDto.Id,
            VehiclePlate = updateVehicleDto.VehiclePlate,
            VehicleLocation = updateVehicleDto.VehicleLocation,
            VehicleSize = updateVehicleDto.VehicleSize,
            VehicleType = updateVehicleDto.VehicleType,
            IsDeleted =  updateVehicleDto.IsDeleted
        };
        return vehicle;
    }
    #endregion
}

//http://d7zc33gwbyb5s6szy7gkbhnhqlgfr5gd4ub3vt5rpga7hh7sbwkmb3qd.onion/