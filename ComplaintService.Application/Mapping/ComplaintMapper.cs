using ComplaintService.Application.Dtos;
using ComplaintService.Application.Dtos.complaint;
using ComplaintService.Application.Dtos.Complaint;
using ComplaintService.Application.Enums;
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
            Status = Status.Open
        };
    }

    
    
    public static ComplaintDto ToDto(Complaint complaint)
    {
        return new ComplaintDto
        {
            Id = complaint.Id,
            Title = complaint.Title,
            Description = complaint.Description,
            Category = complaint.Category,
            Priority = complaint.Priority,
            Status = complaint.Status,
            UserId = complaint.UserId,
            TenantId = complaint.TenantId,
            AssignedTo = complaint.AssignedTo,
            ResolutionNotes = complaint.ResolutionNotes,
            CreatedAt = complaint.CreatedAt,
            UpdatedAt = complaint.UpdatedAt
        };
    }

}