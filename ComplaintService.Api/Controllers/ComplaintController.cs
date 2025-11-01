using ComplaintService.Application.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintController : ControllerBase
{
    private readonly ILogger<ComplaintController> _logger;
    
    public ComplaintController(ILogger<ComplaintController> logger)
    {
        _logger = logger;
    }
    
    [HttpPost]
    public IActionResult CreateComplaint([FromBody] object complaint)
    {
        _logger.LogInformation("Received complaint creation request.");

        return Ok(ApiResponse<string>.Ok("Complaint created successfully"));
    }
}



