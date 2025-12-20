using ComplaintService.Application.Enums;
using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Models;
using ComplaintService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ComplaintService.Infrastructure.Repositories;

public class ComplaintRepository : IComplaintRepository
{
    private readonly ComplaintDbContext _context;

    public ComplaintRepository(ComplaintDbContext context)
    {
        _context = context;
    }
    
    public async Task<Complaint> AddAsync(Complaint complaint)
    {
        _context.Complaints.Add(complaint);
        await _context.SaveChangesAsync();
        return complaint;
    }

    public async Task<List<Complaint>> GetAllAsync()
    {
        return await _context.Complaints
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<Complaint?> GetComplaintById(Guid id)
    { 
        return await _context.Complaints
            .FirstOrDefaultAsync(c => c.Id == id);    
    }

    public async Task<Complaint?> UpdateComplaint(Complaint complaint)
    {
        _context.Complaints.Update(complaint);
        await _context.SaveChangesAsync();
        return complaint;
    }
    
    public async Task<DeleteStatus> DeleteComplaint(Guid id)
    {
        var complaint = await _context.Complaints.FindAsync(id);

        if (complaint == null)
            return DeleteStatus.NotFound;

        _context.Complaints.Remove(complaint);
        await _context.SaveChangesAsync();

        return DeleteStatus.Deleted;
    }

    public async Task<List<Complaint>> GetByUserAndTenantAsync(Guid userId, Guid tenantId)
    {
        return await _context.Complaints
            .Where(c => c.UserId == userId && c.TenantId == tenantId)
            .OrderByDescending(c => c.UpdatedAt ?? c.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<List<Complaint>> GetByTenantAsync(Guid tenantId)
    {
        return await _context.Complaints
            .Where(c => c.TenantId == tenantId)
            .OrderByDescending(c => c.UpdatedAt ?? c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Complaint?> GetByIdAndTenantAsync(Guid id, Guid tenantId)
    {
        return await _context.Complaints
            .FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId);
    }

}