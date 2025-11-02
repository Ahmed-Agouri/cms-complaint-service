using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Models;
using ComplaintService.Infrastructure.Data;

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
}