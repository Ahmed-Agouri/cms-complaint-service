using ComplaintService.Application.Dtos;
using ComplaintService.Application.Dtos.Complaint;
using ComplaintService.Application.Dtos.Resolution;
using ComplaintService.Application.Enums;
using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Mapping;

namespace ComplaintService.Application.Services;

public class ResolutionService : IResolutionService
{
    private readonly IComplaintRepository _complaintRepository;

    public ResolutionService(
        IComplaintRepository complaintRepository
    )
    {
        _complaintRepository = complaintRepository;
    }
    
    public async Task<ComplaintDto?> AddResolutionAsync(Guid complaintId, Guid tenantId,UpdateResolutionDto dto)
    {
        var complaint = await _complaintRepository.GetByIdAndTenantAsync(complaintId, tenantId);
        if (complaint == null) return null;

        complaint.ResolutionNotes = dto.ResolutionNotes;
        complaint.AssignedTo = dto.AssignedTo;
        complaint.Status = Status.Resolved;
        complaint.UpdatedAt = DateTime.UtcNow;

        await _complaintRepository.UpdateComplaint(complaint);
        return ComplaintMapper.ToDto(complaint);
    }
    
    public async Task<ComplaintDto?> UpdateResolutionAsync(Guid complaintId, Guid tenantId,UpdateResolutionDto dto)
    {
        var complaint = await _complaintRepository.GetByIdAndTenantAsync(complaintId, tenantId);

        if (complaint == null)
            return null;

        if (dto.AssignedTo != null)
            complaint.AssignedTo = dto.AssignedTo;

        if (dto.ResolutionNotes != null)
            complaint.ResolutionNotes = dto.ResolutionNotes;

        if (dto.Status != null)
            complaint.Status = dto.Status;

        complaint.UpdatedAt = DateTime.UtcNow;

        await _complaintRepository.UpdateComplaint(complaint);

        return ComplaintMapper.ToDto(complaint);
    }
    
    public async Task<bool> ClearResolutionAsync(Guid complaintId,Guid tenantId)
    {
        var complaint = await _complaintRepository.GetByIdAndTenantAsync(complaintId, tenantId);

        if (complaint == null)
            return false;

        complaint.AssignedTo = null;
        complaint.ResolutionNotes = null;
        complaint.Status = Enums.Status.Open;
        complaint.UpdatedAt = DateTime.UtcNow;

        await _complaintRepository.UpdateComplaint(complaint);

        return true;
    }

    public async Task<ComplaintDto?> ConfirmResolutionAsync(
        Guid complaintId,
        Guid tenantId,
        Guid userId,
        ConfirmResolutionDto dto)
    {
        var complaint = await _complaintRepository.GetByIdAndTenantAsync(complaintId, tenantId);

        if (complaint == null)
            return null;

        if (complaint.UserId != userId)
            throw new UnauthorizedAccessException("User cannot confirm this complaint");

        if (complaint.Status != Status.Resolved)
            throw new InvalidOperationException("Complaint is not resolved yet.");

        complaint.Rating = dto.Rating;
        complaint.Feedback = dto.Feedback;
        complaint.Status = Status.Closed;
        complaint.UpdatedAt = DateTime.UtcNow;

        await _complaintRepository.UpdateComplaint(complaint);
        
        return ComplaintMapper.ToDto(complaint);
    }


}