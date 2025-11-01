using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Dtos;

public class CreateComplaintDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public PriorityLevelEnum Priority { get; set; } = PriorityLevelEnum.Low ;
    public Guid CreatedByUserId { get; set; } 
    public Guid TenantId { get; set; } 
}