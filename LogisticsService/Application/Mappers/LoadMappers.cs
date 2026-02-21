using System.Collections.Immutable;
using LogisticsService.Application.DTOs;
using LogisticsService.Domain.Entities;

namespace LogisticsService.Application.Mappers;

public class LoadMappers
{
    #region EntityToResponseDto
    public static List<LoadResponseDto> EntityToResponseDtoList(List<Loads> loads)
    {
        var result = new List<LoadResponseDto>();
        foreach (var load in loads)
        {
            var loadDto = new LoadResponseDto()
            {
                Id = load.Id,
                LoadStatus = load.LoadStatus,
                CargoType = load.CargoType,
                Location = load.Location,
                PickupDate = load.PickupDate,
                Weight = load.Weight,
                DelieveryLocation = load.DelieveryLocation,
                DeliveryDate = load.DeliveryDate,
                DelieveryContact = load.DelieveryContact,
                DelieveryInstructions =  load.DelieveryInstructions,
                VehicleType = load.VehicleType,
                IsDeleted = load.IsDeleted,
                CreatedByUserId =  load.CreatedByUserId,
                CreatedDate = load.CreatedDate,
            };
            result.Add(loadDto);
        }
        
        return result;
    }
    
    public static LoadResponseDto EntityToResponseDto(Loads? load)
    {
        var loadDto = new LoadResponseDto()
        {
            Id = load.Id,
            LoadStatus = load.LoadStatus,
            CargoType = load.CargoType,
            Location = load.Location,
            PickupDate = load.PickupDate,
            Weight = load.Weight,
            DelieveryLocation = load.DelieveryLocation,
            DeliveryDate = load.DeliveryDate,
            DelieveryContact = load.DelieveryContact,
            DelieveryInstructions =  load.DelieveryInstructions,
            VehicleType = load.VehicleType,
            IsDeleted = load.IsDeleted,
            CreatedByUserId =  load.CreatedByUserId,
            CreatedDate = load.CreatedDate,
        };
        return loadDto;
    }
    
    #endregion

    #region RequestDtoToEntity
    public static List<Loads> DtoToEntityList(List<LoadRequestDto> loadRequestDtos)
    {
        var result = new List<Loads>();
        foreach (var loadDto in loadRequestDtos)
        {
            var load = new Loads()
            {
                Id = loadDto.Id,
                LoadStatus = loadDto.LoadStatus,
                CargoType = loadDto.CargoType,
                Location = loadDto.Location,
                PickupDate = loadDto.PickupDate,
                Weight = loadDto.Weight,
                DelieveryLocation = loadDto.DelieveryLocation,
                DeliveryDate = loadDto.DeliveryDate,
                DelieveryContact = loadDto.DelieveryContact,
                DelieveryInstructions =  loadDto.DelieveryInstructions,
                VehicleType = loadDto.VehicleType
            };
            result.Add(load);
        }
        
        return result;
    }
    
    public static Loads DtoToEntity(LoadRequestDto loadRequestDto)
    {
        var load = new Loads()
        {
            Id = loadRequestDto.Id,
            LoadStatus = loadRequestDto.LoadStatus,
            CargoType = loadRequestDto.CargoType,
            Location = loadRequestDto.Location,
            PickupDate = loadRequestDto.PickupDate,
            Weight = loadRequestDto.Weight,
            DelieveryLocation = loadRequestDto.DelieveryLocation,
            DeliveryDate = loadRequestDto.DeliveryDate,
            DelieveryContact = loadRequestDto.DelieveryContact,
            DelieveryInstructions =  loadRequestDto.DelieveryInstructions,
            VehicleType = loadRequestDto.VehicleType
        };

        return load;
    }
    
    #endregion
    
    #region CreateDtoToEntity
    public static Loads CreateDtoToEntity(CreateLoadDto? createLoadDto, Guid createdByUserId)
    {
        var load = new Loads()
        {
            CreatedByUserId = createdByUserId,
            LoadStatus = createLoadDto.LoadStatus,
            CargoType = createLoadDto.CargoType,
            Location = createLoadDto.Location,
            PickupDate = createLoadDto.PickupDate,
            Weight = createLoadDto.Weight,
            DelieveryLocation = createLoadDto.DelieveryLocation,
            DeliveryDate = createLoadDto.DeliveryDate,
            DelieveryContact = createLoadDto.DelieveryContact,
            DelieveryInstructions =  createLoadDto.DelieveryInstructions,
            VehicleType = createLoadDto.VehicleType,
        };
        return load;
    }
    #endregion
    
    #region UpdateDtoToEntity
    public static Loads UpdateDtoToEntity(UpdateLoadDto? updateLoadDto, Guid loadId)
    {
        var load = new Loads()
        {
            Id = loadId,
            LoadStatus = updateLoadDto.LoadStatus,
            CargoType = updateLoadDto.CargoType,
            Location = updateLoadDto.Location,
            PickupDate = updateLoadDto.PickupDate,
            Weight = updateLoadDto.Weight,
            DelieveryLocation = updateLoadDto.DelieveryLocation,
            DeliveryDate = updateLoadDto.DeliveryDate,
            DelieveryContact = updateLoadDto.DelieveryContact,
            DelieveryInstructions =  updateLoadDto.DelieveryInstructions,
            VehicleType = updateLoadDto.VehicleType,
        };
        return load;
    }
    #endregion
    
    
}