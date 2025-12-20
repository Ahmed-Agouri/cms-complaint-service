using ComplaintService.Application.Dtos;
using ComplaintService.Application.Dtos.complaint;
using ComplaintService.Application.Dtos.Complaint;
using ComplaintService.Application.Enums;
using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Mapping;
using ComplaintService.Application.Models;

namespace ComplaintService.Application.Services;

public class ComplaintService : IComplaintService
{
      private readonly IComplaintRepository _complaintRepository;

      public ComplaintService(
            IComplaintRepository complaintRepository
      )
      {
            _complaintRepository = complaintRepository;
      }
      
      public async Task<ComplaintDto?> CreateComplaintAsync(CreateComplaintDto dto,Guid tenantId,Guid userId)
      {
            try
            {
                  var complaint = ComplaintMapper.CreateDtoToEntity(dto);
                  complaint.Id = Guid.NewGuid();
                  complaint.UserId = userId;
                  complaint.TenantId = tenantId;
                  complaint.Status = Status.Open;
                  complaint.CreatedAt = DateTime.UtcNow;
                  complaint.UpdatedAt = DateTime.UtcNow;

                  await _complaintRepository.AddAsync(complaint);

                  return ComplaintMapper.ToDto(complaint);
            }
            catch (Exception e)
            {
                  Console.WriteLine($"[Error] Failed to create complaint: {e.Message}");
                  return null;
            }
      }

      public async Task<List<ComplaintDto>> GetComplaintsByUserAsync(Guid userId, Guid tenantId)
      {
            var complaints = await _complaintRepository.GetByUserAndTenantAsync(userId, tenantId);

            return complaints.Select(ComplaintMapper.ToDto).ToList();
      }
      
      
      public async Task<List<ComplaintDto?>> GetComplaintsAsync(Guid tenantId)
      {
            try
            {
                  var complaints = await _complaintRepository.GetByTenantAsync(tenantId);
                  return complaints.Select(ComplaintMapper.ToDto).ToList();
            }
            catch (Exception e)
            {
                  Console.WriteLine($"[Error] Failed to fetch complaints: {e.Message}");
                  throw;
            }
      }

      public async Task<ComplaintDto?> GetComplaintByIdAsync(Guid id, Guid tenantId)
      {
            try
            {
                  var complaint = await _complaintRepository.GetByIdAndTenantAsync(id, tenantId);
                  return complaint == null ? null : ComplaintMapper.ToDto(complaint);
            }
            catch (Exception e)
            {
                  Console.WriteLine($"[Error] Failed to fetch complaint by ID ({id}): {e.Message}");
                  throw;
            }
      }

      public async Task<ComplaintDto?> UpdateComplaintAsync(Guid id, UpdateComplaintDto complaintDto, Guid tenantId)
      {
            try
            {
                  var existing = await _complaintRepository.GetByIdAndTenantAsync(id, tenantId);

                  if (existing == null)
                        return null;

                  ApplyUpdates(existing, complaintDto);
                  var updated = await _complaintRepository.UpdateComplaint(existing);

                  return ComplaintMapper.ToDto(updated);
            }
            catch (Exception e)
            {
                  Console.WriteLine($"[Error] Failed to update complaint ({id}): {e.Message}");
                  throw;
            }
      }

      public Task<DeleteStatus> DeleteComplaintAsync(Guid id,Guid tenantId)
      {
            try
            {
                  return _complaintRepository.DeleteComplaint(id);
            }
            catch (Exception e)
            {
                  Console.WriteLine($"[Error] Failed to Delete complaint by id ({id}): {e.Message}");
                  throw;
            }
      }
      

      private static void ApplyUpdates(Complaint entity, UpdateComplaintDto dto)
      {
            if (!string.IsNullOrWhiteSpace(dto.Title))
                  entity.Title = dto.Title;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                  entity.Description = dto.Description;

            if (dto.Priority.HasValue)
                  entity.Priority = dto.Priority.Value;

            if (dto.Category.HasValue)
                  entity.Category = dto.Category.Value;

            if (dto.Status.HasValue)
                  entity.Status = dto.Status.Value;

            if (!string.IsNullOrWhiteSpace(dto.AssignedTo))
                  entity.AssignedTo = dto.AssignedTo;

            if (!string.IsNullOrWhiteSpace(dto.ResolutionNotes))
                  entity.ResolutionNotes = dto.ResolutionNotes;

            entity.UpdatedAt = DateTime.UtcNow;
      }


}