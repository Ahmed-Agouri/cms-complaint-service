using ComplaintService.Application.Models;

namespace ComplaintService.Application.Interfaces;

public interface IComplaintRepository
{
    Task<Complaint?> GetByIdAsync(Guid id);
    Task<IEnumerable<Complaint>> GetAllByTenantAsync(Guid tenantId);
    Task AddAsync(Complaint complaint);
    Task SaveChangesAsync();
    Task UpdateAsync(Complaint complaint);
    Task DeleteAsync(Complaint complaint);
}