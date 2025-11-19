using ComplaintService.Application.Dtos;
using ComplaintService.Application.Enums;
using ComplaintService.Application.Models;

namespace ComplaintService.Application.Interfaces;

public interface IComplaintService
{
    public Task<CreateComplaintDto?> CreateComplaintAsync(CreateComplaintDto createComplaintDto);
    public Task<List<ComplaintDto?>> GetComplaintsAsync();
    public Task<ComplaintDto> GetComplaintByIdAsync(Guid id);
    public Task<ComplaintDto> UpdateComplaintAsync(Guid id,UpdateComplaintDto updateComplaintDto );
    public Task<DeleteStatus> DeleteComplaintAsync(Guid id);
}