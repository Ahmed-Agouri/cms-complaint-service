using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Models;

public class Complaint
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public PriorityLevel Priority { get; set; }
    public ComplaintCategory Category { get; set; }
    public Guid TenantId { get; set; }
    public string? AssignedTo { get; set; }
    public string? ResolutionNotes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? Rating { get; set; }
    public string? Feedback { get; set; }

    public Complaint() { }
    
}