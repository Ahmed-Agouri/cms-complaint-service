using ComplaintService.Application.Dtos;
using ComplaintService.Application.Models;

namespace ComplaintService.Application.Mapping;

public static class ComplaintMapper
{
    public static Complaint ToEntity(CreateComplaintDto dto)
    {
        return new Complaint
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            TenantId = dto.TenantId,
            CreatedByUserId = dto.UserId,
            Category = dto.Category,
            CreatedAt = DateTime.UtcNow,
            Status = Enums.Status.Open
        };
    }
}