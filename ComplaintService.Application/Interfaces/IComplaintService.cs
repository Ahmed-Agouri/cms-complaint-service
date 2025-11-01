using ComplaintService.Application.Dtos;
using ComplaintService.Application.Models;

namespace ComplaintService.Application.Interfaces;

public interface IComplaintService
{
    public Task<ComplaintDto?> CreateComplaintAsync(CreateComplaintDto dto);
}