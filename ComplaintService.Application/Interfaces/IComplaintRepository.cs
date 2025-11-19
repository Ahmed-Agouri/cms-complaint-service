using ComplaintService.Application.Enums;
using ComplaintService.Application.Models;

namespace ComplaintService.Application.Interfaces;

public interface IComplaintRepository
{
     Task<Complaint> AddAsync(Complaint complaint);
     Task<List<Complaint>> GetAllAsync();
     Task<Complaint?> GetComplaintById(Guid id);
     Task<Complaint?> UpdateComplaint(Complaint complaint);
     Task<DeleteStatus> DeleteComplaint(Guid id);
}