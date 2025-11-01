using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Dtos;

public class UpdateComplaintDto : CreateComplaintDto
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
    public PriorityLevel? Priority { get; set; }
    public ComplaintCategory? Category { get; set; }
    public string? AssignedTo { get; set; }
    public string? ResolutionNotes { get; set; }
    public DateTime? UpdatedAt { get; set; }
}