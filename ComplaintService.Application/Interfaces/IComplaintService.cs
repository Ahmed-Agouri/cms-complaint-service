using ComplaintService.Application.Dtos;
using ComplaintService.Application.Dtos.complaint;
using ComplaintService.Application.Dtos.Complaint;
using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Interfaces;

public interface IComplaintService
{
    Task<ComplaintDto?> CreateComplaintAsync(CreateComplaintDto dto,Tenant tenantId);
    Task<List<ComplaintDto?>> GetComplaintsAsync(Tenant tenantId);
    Task<ComplaintDto?> GetComplaintByIdAsync(Guid id, Tenant tenantId);
    Task<ComplaintDto?> UpdateComplaintAsync(Guid id, UpdateComplaintDto complaintDto, Tenant tenantId);
    Task<DeleteStatus> DeleteComplaintAsync(Guid id,Tenant tenantId);
}