using ComplaintService.Application.Dtos;
using ComplaintService.Application.Dtos.Complaint;
using ComplaintService.Application.Dtos.Resolution;

namespace ComplaintService.Application.Interfaces;

public interface IResolutionService
{
     Task<ComplaintDto?> AddResolutionAsync(Guid complaintId, UpdateResolutionDto dto);
     Task<ComplaintDto?> UpdateResolutionAsync(Guid complaintId, UpdateResolutionDto dto);
     Task<bool> ClearResolutionAsync(Guid complaintId);
}