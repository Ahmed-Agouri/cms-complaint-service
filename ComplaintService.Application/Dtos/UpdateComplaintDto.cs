using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Dtos;

public class UpdateComplaintDto : CreateComplaintDto
{
    public Guid Id { get; set; }

    public StatusEnum Status { get; set; }
    public string? AssignedTo { get; set; } // optional field for internal users
    public DateTime? UpdatedAt { get; set; } // handled automatically in service
}