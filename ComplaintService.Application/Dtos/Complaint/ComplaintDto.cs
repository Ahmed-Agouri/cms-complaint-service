using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Dtos.Complaint;

public class ComplaintDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ComplaintCategory Category { get; set; } = ComplaintCategory.Other;
    public PriorityLevel Priority { get; set; }
    public Status Status { get; set; }
    public Guid UserId { get; set; }
    public Tenant TenantId { get; set; }
    public string? AssignedTo { get; set; }
    public string? ResolutionNotes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}