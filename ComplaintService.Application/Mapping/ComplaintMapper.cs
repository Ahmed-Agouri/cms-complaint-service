using ComplaintService.Application.Dtos;
using ComplaintService.Application.Models;

namespace ComplaintService.Application.Mapping;

public static class ComplaintMapper
{
    public static Complaint CreateDtoToEntity(CreateComplaintDto dto)
    {
        return new Complaint
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            TenantId = dto.TenantId,
            UserId = dto.UserId,
            Category = dto.Category,
            CreatedAt = DateTime.UtcNow,
            Status = Enums.Status.Open
        };
    }

    public static CreateComplaintDto ToDto(Complaint complaint)
    {
        return new CreateComplaintDto
        {
            Title = complaint.Title,
            Description = complaint.Description,
            Category = complaint.Category,
            Priority = complaint.Priority,
            UserId = complaint.UserId,
            TenantId = complaint.TenantId,
        };
    }
    
    public static void UpdateEntity(Complaint existingComplaint, UpdateComplaintDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.Title))
            existingComplaint.Title = dto.Title;

        if (!string.IsNullOrWhiteSpace(dto.Description))
            existingComplaint.Description = dto.Description;

        if (dto.Priority.HasValue)
            existingComplaint.Priority = dto.Priority.Value;

        if (dto.Category.HasValue)
            existingComplaint.Category = dto.Category.Value;

        if (dto.Status.HasValue)
            existingComplaint.Status = dto.Status.Value;

        if (!string.IsNullOrWhiteSpace(dto.AssignedTo))
            existingComplaint.AssignedTo = dto.AssignedTo;

        if (!string.IsNullOrWhiteSpace(dto.ResolutionNotes))
            existingComplaint.ResolutionNotes = dto.ResolutionNotes;

        existingComplaint.UpdatedAt = DateTime.UtcNow;
    }

}