using System.Security.Claims;
using ComplaintService.Application.Dtos;
using ComplaintService.Application.Dtos.Complaint;
using ComplaintService.Application.Dtos.Resolution;
using ComplaintService.Application.Enums;
using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResolutionController:ControllerBase
{
    private readonly IResolutionService _resolutionService;
    private readonly IAuditClient _auditClient;


    public ResolutionController(
        IResolutionService resolutionService,
        IAuditClient auditClient

        )
    {
        _resolutionService = resolutionService;
        _auditClient = auditClient; 
    }
    
    
    [HttpPost("{id:guid}")]
    public async Task<IActionResult> AddResolution(Guid id, 
        [FromHeader(Name = "X-Tenant-Id")] Guid tenantId,
        [FromBody] UpdateResolutionDto dto)
    {
        var result = await _resolutionService.AddResolutionAsync(id,tenantId, dto);

        if (result == null)
            return NotFound(ApiResponse<string>.Fail("Complaint not found"));
        
        try
        {
            await _auditClient.RecordAsync(new CreateAuditEntryDto
            {
                TenantId = tenantId,
                UserId = null,
                ComplaintId = result.Id,
                ActionType = "ResolutionAdded",
                Description = $"Resolution was added to complaint '{result.Title}'"
            });
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
        
        return Ok(ApiResponse<ComplaintDto>.Ok(result));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateResolution(Guid id, 
        [FromHeader(Name = "X-Tenant-Id")] Guid tenantId,
        [FromBody] UpdateResolutionDto dto)
    {
        var result = await _resolutionService.UpdateResolutionAsync(id, tenantId,dto);

        if (result == null)
            return NotFound(ApiResponse<string>.Fail("Complaint not found"));

        try
        {
            await _auditClient.RecordAsync(new CreateAuditEntryDto
            {
                TenantId = tenantId,
                UserId = null,
                ComplaintId = result.Id,
                ActionType = "ResolutionAdded",
                Description = $"Resolution was Updated on complaint '{result.Title}'"
            });
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
        
        return Ok(ApiResponse<ComplaintDto>.Ok(result));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> ClearResolution(Guid id,[FromHeader(Name = "X-Tenant-Id")] Guid tenantId)
    {
        var success = await _resolutionService.ClearResolutionAsync(id, tenantId);

        if (!success)
            return NotFound(ApiResponse<string>.Fail("Complaint not found"));

        return Ok(ApiResponse<string>.Ok("Resolution cleared"));
    }
    
    [Authorize]
    [HttpPost("{id:guid}/confirm")]
    public async Task<IActionResult> ConfirmResolution(
        Guid id,
        [FromBody] ConfirmResolutionDto dto,
        [FromHeader(Name="X-Tenant-Id")] Guid tenantId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var result = await _resolutionService.ConfirmResolutionAsync(
            id, tenantId, userId, dto);

        if (result == null)
            return BadRequest("Unable to confirm resolution");

        try
        {
            await _auditClient.RecordAsync(new CreateAuditEntryDto
            {
                TenantId = tenantId,
                UserId = userId,
                ComplaintId = id,
                ActionType = "ResolutionConfirmed",
                Description = $"Resolution confirmed with rating {dto.Rating}"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Ok(ApiResponse<ComplaintDto>.Ok(result));
    }
}