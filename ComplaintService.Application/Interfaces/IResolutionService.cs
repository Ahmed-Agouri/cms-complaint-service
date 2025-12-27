using ComplaintService.Application.Dtos;
using ComplaintService.Application.Dtos.Complaint;
using ComplaintService.Application.Dtos.Resolution;
using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Interfaces;

public interface IResolutionService
{
     Task<ComplaintDto?> AddResolutionAsync(Guid complaintId, Guid tenantId,UpdateResolutionDto dto);
     Task<ComplaintDto?> UpdateResolutionAsync(Guid complaintId, Guid tenantId,UpdateResolutionDto dto);
     Task<bool> ClearResolutionAsync(Guid complaintId, Guid tenantId);
     Task<ComplaintDto?> ConfirmResolutionAsync( Guid complaintId, Guid tenantId, Guid userId, ConfirmResolutionDto dto );
}