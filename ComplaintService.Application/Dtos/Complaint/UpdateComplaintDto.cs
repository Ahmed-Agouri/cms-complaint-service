using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Dtos;

public class UpdateComplaintDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ComplaintCategory? Category { get; set; }
    public PriorityLevel? Priority { get; set; }
    public Status? Status { get; set; }

    public string? AssignedTo { get; set; }
    public string? ResolutionNotes { get; set; }
}