using ComplaintService.Application.Dtos;
using ComplaintService.Application.Enums;
using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintController : ControllerBase
{
    private readonly ILogger<ComplaintController> _logger;
    private readonly IComplaintService _complaintService;
    
    public ComplaintController(
        ILogger<ComplaintController> logger,
        IComplaintService complaintService
        
        )
    {
        _logger = logger;
        _complaintService = complaintService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateComplaint([FromBody] ComplaintDto complaint)
    {
        var result = await _complaintService.CreateComplaintAsync(complaint);

        if (result == null)
        {
        }
        
        return Ok(ApiResponse<string>.Ok("Complaint created successfully"));

    }

    [HttpGet]
    public async Task<IActionResult> GetAllComplaints()
    {
        var result = await _complaintService.GetComplaintsAsync();

        if (result == null || !result.Any())
        {
            return NotFound(ApiResponse<string>.Fail("No complaints found."));
        }
        
        return Ok(ApiResponse<List<ComplaintDto>>.Ok(result));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetComplaintById(Guid id)
    {
        var result = await _complaintService.GetComplaintByIdAsync(id);

        if (result == null)
        {
            return NotFound(ApiResponse<string>.Fail($"No complaint with Id ({id}) found."));
        }
        
        return Ok(ApiResponse<ComplaintDto>.Ok(result));
    }

    [HttpPost("{id:guid}")]

    public async Task<IActionResult> UpdateComplaint(Guid id, ComplaintDto complaint)
    {
        var result = await _complaintService.UpdateComplaintAsync(id, complaint);

        if (result == null)
        {
            return NotFound(ApiResponse<string>.Fail($"No complaint with Id ({id}) found."));
        }

        return Ok(ApiResponse<ComplaintDto>.Ok(result));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteComplaint(Guid id)
    {
        var result = await _complaintService.DeleteComplaintAsync(id);

        if (result == null)
        {
            return NotFound(ApiResponse<string>.Fail($"No complaint with Id ({id}) found."));
        }

        return Ok(ApiResponse<DeleteStatus>.Ok(result));
    }
}



