using ComplaintService.Application.Dtos;
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
    public async Task<IActionResult> CreateComplaint([FromBody] CreateComplaintDto complaint)
    {
        var result = await _complaintService.CreateComplaintAsync(complaint);

        if (result == null)
        {
            return BadRequest(ApiResponse<string>.Fail("Failed to create complaint."));
        }
        
        return Ok(ApiResponse<string>.Ok("Complaint created successfully"));

    }
}



