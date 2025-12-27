using ComplaintService.Application.Dtos;
using ComplaintService.Application.Dtos.complaint;
using ComplaintService.Application.Dtos.Complaint;
using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Interfaces;

public interface IComplaintService
{
    Task<ComplaintDto?> CreateComplaintAsync(CreateComplaintDto dto,Guid tenantId,Guid userId);
    Task<List<ComplaintDto>> GetComplaintsByUserAsync(Guid userId,Guid tenantI);
    Task<List<ComplaintDto?>> GetComplaintsAsync(Guid tenantId);
    Task<ComplaintDto?> GetComplaintByIdAsync(Guid id, Guid tenantId);
    Task<ComplaintDto?> UpdateComplaintAsync(Guid id, UpdateComplaintDto complaintDto, Guid tenantId);
    Task<DeleteStatus> DeleteComplaintAsync(Guid id,Guid tenantId);
}