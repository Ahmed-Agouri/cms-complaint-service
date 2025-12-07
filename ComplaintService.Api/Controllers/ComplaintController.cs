using ComplaintService.Application.Dtos;
using ComplaintService.Application.Dtos.complaint;
using ComplaintService.Application.Dtos.Complaint;
using ComplaintService.Application.Enums;
using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintController : ControllerBase
{
    private readonly IComplaintService _complaintService;
    
    public ComplaintController(
        IComplaintService complaintService
        )
    {
        _complaintService = complaintService;
    }
    
[HttpPost]
public async Task<IActionResult> CreateComplaint(
    [FromBody] CreateComplaintDto dto,
    [FromHeader(Name = "X-Tenant-Id")] Guid tenantId)
{
    var result = await _complaintService.CreateComplaintAsync(dto, tenantId);

    if (result == null)
        return BadRequest(ApiResponse<string>.Fail("Failed to create complaint."));

    return Ok(ApiResponse<ComplaintDto>.Ok(result));
}

    [HttpGet]
    public async Task<IActionResult> GetAllComplaints([FromHeader(Name = "X-Tenant-Id")] Guid tenantId)
    {
        var result = await _complaintService.GetComplaintsAsync(tenantId);

        if (!result.Any())
        {
            return NotFound(ApiResponse<string>.Fail("No complaints found."));
        }

        return Ok(ApiResponse<List<ComplaintDto>>.Ok(result));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetComplaintById(Guid id, [FromHeader(Name = "X-Tenant-Id")] Guid tenantId)
    {
        var result = await _complaintService.GetComplaintByIdAsync(id, tenantId);

        if (result == null)
        {
            return NotFound(ApiResponse<string>.Fail($"No complaint with Id ({id}) found."));
        }
        
        return Ok(ApiResponse<ComplaintDto>.Ok(result));
    }

    [HttpPut("{id:guid}")]

    public async Task<IActionResult> UpdateComplaint(Guid id, UpdateComplaintDto complaintDto,
        [FromHeader(Name = "X-Tenant-Id")] Guid tenantId)
    {
        var result = await _complaintService.UpdateComplaintAsync(id, complaintDto, tenantId);

        if (result == null)
        {
            return NotFound(ApiResponse<string>.Fail($"No complaint with Id ({id}) found."));
        }

        return Ok(ApiResponse<ComplaintDto>.Ok(result));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteComplaint(Guid id,[FromHeader(Name = "X-Tenant-Id")] Guid tenantId )
    {
        var result = await _complaintService.DeleteComplaintAsync(id,tenantId);

        if (result == null)
        {
            return NotFound(ApiResponse<string>.Fail($"No complaint with Id ({id}) found."));
        }

        return Ok(ApiResponse<DeleteStatus>.Ok(result));
    }
}



