using ComplaintService.Application.Models;

namespace ComplaintService.Application.Interfaces;

public interface IComplaintRepository
{
     Task<Complaint> AddAsync(Complaint complaint);

}