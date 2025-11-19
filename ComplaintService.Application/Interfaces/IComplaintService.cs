using ComplaintService.Application.Dtos;
using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Interfaces;

public interface IComplaintService
{
    Task<ComplaintDto?> CreateComplaintAsync(CreateComplaintDto dto);
    Task<List<ComplaintDto?>> GetComplaintsAsync();
    Task<ComplaintDto?> GetComplaintByIdAsync(Guid id);
    Task<ComplaintDto?> UpdateComplaintAsync(Guid id, UpdateComplaintDto complaintDto);
    Task<DeleteStatus> DeleteComplaintAsync(Guid id);
}