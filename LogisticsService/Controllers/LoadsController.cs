using System.Security.Claims;
using LogisticsService.Application.DTOs;
using LogisticsService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LoadsController(ILoadsService loadsService) : ControllerBase
{
    private readonly ILoadsService _loadsService = loadsService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateLoadDto createLoadDto, CancellationToken token)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim is null)
            return Unauthorized();
        
        createLoadDto.CreatedByUserId = new Guid(userIdClaim);
        
        var result = await _loadsService.CreateLoadAsync(createLoadDto, token);
        return Ok(result.result); // RegisterUserDto returned
    }

    [HttpGet("{loadId:guid}")]
    public async Task<IActionResult> GetById(Guid loadId, CancellationToken token)
    {
        var loadDto = await _loadsService.GetLoadsAsync(loadId, token);
        if(loadDto == null)
            return NotFound();
        return Ok(loadDto);
    }
    
    [HttpPut("{loadId:guid}")]
    public async Task<IActionResult> Update(Guid loadId, [FromBody] LoadDto dto, CancellationToken token)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null || loadId != dto.Id)
            return Unauthorized();

        var updated = await _loadsService.UpdateLoadAsync(dto, token);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }
    
    [HttpDelete("{loadId:guid}")]
    public async Task<IActionResult> Delete(Guid loadId, CancellationToken token)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim is null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);
        var success = await _loadsService.DeleteLoadAsync(loadId, userId, token);

        if (!success)
            return NotFound();

        return Ok();
    }
}