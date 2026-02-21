using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using LogisticsService.Application.DTOs;
using LogisticsService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VehiclesController(IVehiclesService vehiclesService) : ControllerBase
{
    private readonly IVehiclesService _vehiclesService = vehiclesService;

    [HttpGet("{vehicleId:guid}")]
    public async Task<IActionResult> GetById(Guid vehicleId, CancellationToken token)
    {
        var vehicleDto = await _vehiclesService.GetVehiclesAsync(vehicleId, token);
        if (vehicleDto == null)
            return NotFound();
        return Ok(vehicleDto);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var vehicleDtos = await _vehiclesService.GetAllVehiclesAsync(token);
        if (!vehicleDtos.Any())
            return NotFound();
        return Ok(vehicleDtos);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehicleDto createVehicleDto, CancellationToken token)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim is null)
            return Unauthorized();

        var ownerUserId = new Guid(userIdClaim);

        var result = await _vehiclesService.CreateVehicleAsync(createVehicleDto, ownerUserId, token);
        
        if (!result.result)
        {
            return BadRequest("Error when creating Vehicle.");
        }
        
        return Ok(result.Item1); // VehicleResponseDto returned
    }

    [HttpPut("{vehicleId:guid}")]
    public async Task<IActionResult> Update(Guid vehicleId, [FromBody] UpdateVehicleDto requestDto,
        CancellationToken token)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var vehicle = await _vehiclesService.GetVehiclesAsync(vehicleId, token);
        
        if (userId is null || userId != vehicle?.OwnerUserId.ToString())
            return Unauthorized();

        var updated = await vehiclesService.UpdateVehicleAsync(requestDto, vehicleId, token);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }
    
    [HttpDelete("{vehicleId:guid}")]
    public async Task<IActionResult> Delete(Guid vehicleId, CancellationToken token)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var vehicle = await _vehiclesService.GetVehiclesAsync(vehicleId, token);
        
        if (userIdClaim is null || userIdClaim !=  vehicle?.OwnerUserId.ToString())
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);
        var success = await vehiclesService.DeleteVehicleAsync(vehicleId, userId, token);

        if (!success)
            return NotFound();

        return Ok(success);
    }
}