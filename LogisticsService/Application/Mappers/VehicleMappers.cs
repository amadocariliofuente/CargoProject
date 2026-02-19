using System.Collections.Immutable;
using LogisticsService.Application.DTOs;
using LogisticsService.Domain.Entities;

namespace LogisticsService.Application.Mappers;

public class LoadMappers
{
    public static List<LoadDto> EntityToDtoList(List<Loads> loads)
    {
        var result = new List<LoadDto>();
        foreach (var load in loads)
        {
            var loadDto = new LoadDto()
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

    public static List<Loads> DtoToEntityList(List<LoadDto> loadDtos)
    {
        var result = new List<Loads>();
        foreach (var loadDto in loadDtos)
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
                VehicleType = loadDto.VehicleType,
                IsDeleted = loadDto.IsDeleted,
                CreatedByUserId =  loadDto.CreatedByUserId,
                CreatedDate = loadDto.CreatedDate,
            };
            result.Add(load);
        }
        
        return result;
    }
    
    public static Loads DtoToEntity(LoadDto loadDto)
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
            VehicleType = loadDto.VehicleType,
            IsDeleted = loadDto.IsDeleted,
            CreatedByUserId =  loadDto.CreatedByUserId,
            CreatedDate = loadDto.CreatedDate,
        };

        return load;
    }
    
    public static LoadDto EntityToDto(Loads? load)
    {
        var loadDto = new LoadDto()
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
    
    public static CreateLoadDto EntityToCreateDto(Loads? load)
    {
        var createLoadDto = new CreateLoadDto()
        {
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
            CreatedByUserId =  load.CreatedByUserId,
            CreatedDate = load.CreatedDate,
        };
        return createLoadDto;
    }
    
    public static Loads CreateDtoToEntity(CreateLoadDto? createLoadDto)
    {
        var load = new Loads()
        {
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
            CreatedByUserId =  createLoadDto.CreatedByUserId,
            CreatedDate = createLoadDto.CreatedDate,
        };
        return load;
    }
}