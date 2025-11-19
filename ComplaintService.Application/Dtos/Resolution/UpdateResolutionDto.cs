using System.ComponentModel.DataAnnotations;
using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Dtos.Resolution;

public class UpdateResolutionDto
{
    [Required]
    public string ResolutionNotes { get; set; } = string.Empty;
    [Required]
    
    public string? AssignedTo { get; set; }
    
    public Status Status { get; set; } = Status.Resolved;
}