using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Dtos.complaint;

public class CreateComplaintDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
    public ComplaintCategory Category { get; set; } = ComplaintCategory.Other;
}