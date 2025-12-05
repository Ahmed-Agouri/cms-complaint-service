using ComplaintService.Application.Dtos.Complaint;
using ComplaintService.Application.Dtos.Resolution;
using ComplaintService.Application.Enums;
using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResolutionController:ControllerBase
{
    private readonly IResolutionService _resolutionService;

    public ResolutionController(IResolutionService resolutionService)
    {
        _resolutionService = resolutionService;
    }
    
    
    [HttpPost("{id:guid}")]
    public async Task<IActionResult> AddResolution(Guid id, Tenant tenantId,[FromBody] UpdateResolutionDto dto)
    {
        var result = await _resolutionService.AddResolutionAsync(id,tenantId, dto);

        if (result == null)
            return NotFound(ApiResponse<string>.Fail("Complaint not found"));

        return Ok(ApiResponse<ComplaintDto>.Ok(result));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateResolution(Guid id, Tenant tenantId,[FromBody] UpdateResolutionDto dto)
    {
        var result = await _resolutionService.UpdateResolutionAsync(id, tenantId,dto);

        if (result == null)
            return NotFound(ApiResponse<string>.Fail("Complaint not found"));

        return Ok(ApiResponse<ComplaintDto>.Ok(result));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> ClearResolution(Guid id,Tenant tenantId)
    {
        var success = await _resolutionService.ClearResolutionAsync(id, tenantId);

        if (!success)
            return NotFound(ApiResponse<string>.Fail("Complaint not found"));

        return Ok(ApiResponse<string>.Ok("Resolution cleared"));
    }
}