using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Models;

public class Complaint
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public Status Status { get; set; }
    
    public PriorityLevel? Priority { get; set; }
    
    public ComplaintCategory? Category { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public string? ResolutionNotes { get; set; }

    
    public Complaint(string title, string description)
    {
        Title = title;
        Description = description;
        Status = Status.Open;
    }
    
}