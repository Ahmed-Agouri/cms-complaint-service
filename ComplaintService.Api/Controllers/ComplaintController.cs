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
    public IActionResult CreateComplaint([FromBody] CreateComplaintDto complaint)
    {
        var result = _complaintService.CreateComplaint(CreateComplaintDto);

        if (result)
        {
            return Ok(ApiResponse<string>.Ok("Complaint created successfully"));

        }
        return Ok(ApiResponse<string>.Ok("Complaint created successfully"));

    }
}



