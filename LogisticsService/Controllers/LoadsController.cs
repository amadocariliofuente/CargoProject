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

    [HttpGet("{loadId:guid}")]
    public async Task<IActionResult> GetById(Guid loadId, CancellationToken token)
    {
        var loadDto = await _loadsService.GetLoadAsync(loadId, token);
        if(loadDto == null)
            return NotFound();
        return Ok(loadDto);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var loadDtos = await _loadsService.GetAllLoadsAsync(token);
        if(!loadDtos.Any())
            return NotFound();
        return Ok(loadDtos);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateLoadDto createLoadDto, CancellationToken token)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim is null)
            return Unauthorized();
        
        var createdByUserId = new Guid(userIdClaim);
        
        var result = await _loadsService.CreateLoadAsync(createLoadDto, createdByUserId, token);
        
        if (!result.result)
        {
            return BadRequest("Error when creating Loads.");
        }
        
        return Ok(result.Item1); // LoadResponseDto returned
    }
    
    [HttpPut("{loadId:guid}")]
    public async Task<IActionResult> Update(Guid loadId, [FromBody] UpdateLoadDto updateLoadDto, CancellationToken token)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var load = await _loadsService.GetLoadAsync(loadId, token);
        
        if (userId is null || load!.CreatedByUserId.ToString() != userId)
            return Unauthorized();

        var updated = await _loadsService.UpdateLoadAsync(updateLoadDto, loadId, token);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }
    
    [HttpDelete("{loadId:guid}")]
    public async Task<IActionResult> Delete(Guid loadId, CancellationToken token)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var load = await _loadsService.GetLoadAsync(loadId, token);
        
        if (userIdClaim is null|| load!.CreatedByUserId.ToString() != userIdClaim)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);
        var success = await _loadsService.DeleteLoadAsync(loadId, userId, token);

        if (!success)
            return NotFound();

        return Ok(success);
    }
}