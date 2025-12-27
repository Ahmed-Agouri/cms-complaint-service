using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Dtos.Resolution;

public class ResolutionDto
{
    public Guid ComplaintId { get; set; }
    public string ResolutionNotes { get; set; } = string.Empty;
    public string? AssignedTo { get; set; }
    public Status Status { get; set; }
    public DateTime UpdatedAt { get; set; }
    
}