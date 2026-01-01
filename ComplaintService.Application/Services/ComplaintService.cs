using ComplaintService.Application.Dtos;
using ComplaintService.Application.Dtos.ClientDtos;
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
      private readonly IAuditClient _auditClient;
      private readonly ITenantClient _tenantClient;

      public ComplaintService(
            IComplaintRepository complaintRepository,
            IAuditClient auditClient,
            ITenantClient tenantClient
      )
      {
            _complaintRepository = complaintRepository;
            _auditClient = auditClient; 
            _tenantClient = tenantClient;
      }
      
      public async Task<ComplaintDto?> CreateComplaintAsync(CreateComplaintDto dto,Guid tenantId,Guid userId)
      { 
            await ValidateTenant(tenantId);
            try
            {
                  var complaint = ComplaintMapper.CreateDtoToEntity(dto);
                  complaint.ComplaintId = Guid.NewGuid();
                  complaint.UserId = userId;
                  complaint.TenantId = tenantId;
                  complaint.Status = Status.Open;
                  complaint.CreatedAt = DateTime.UtcNow;
                  complaint.UpdatedAt = DateTime.UtcNow;

                  await _complaintRepository.AddAsync(complaint);
                  try
                  {
                        await _auditClient.RecordAsync(new CreateAuditEntryDto
                        {
                              TenantId = tenantId,
                              UserId = userId,
                              ComplaintId = complaint.ComplaintId,
                              ActionType = "ComplaintCreated",
                              Description = $"Complaint '{complaint.Title}' was created"
                        });
                  }
                  catch(Exception e)
                  {
                        Console.WriteLine(e);
                  }

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
            await ValidateTenant(tenantId);

            var complaints = await _complaintRepository.GetByUserAndTenantAsync(userId, tenantId);

            return complaints.Select(ComplaintMapper.ToDto).ToList();
      }
      
      
      public async Task<List<ComplaintDto?>> GetComplaintsAsync(Guid tenantId)
      {
            await ValidateTenant(tenantId);

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
            await ValidateTenant(tenantId);

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
            await ValidateTenant(tenantId);

            try
            {
                  var existing = await _complaintRepository.GetByIdAndTenantAsync(id, tenantId);

                  if (existing == null)
                        return null;

                  ApplyUpdates(existing, complaintDto);
                  var updated = await _complaintRepository.UpdateComplaint(existing);
                  
                  try
                  {
                        await _auditClient.RecordAsync(new CreateAuditEntryDto
                        {
                              TenantId = tenantId,
                              ComplaintId = updated.ComplaintId,
                              ActionType = "ComplaintCreated",
                              Description = $"Complaint '{updated.Title}' was Updated"
                        });
                  }
                  catch(Exception e)
                  {
                        Console.WriteLine(e);
                  }

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

      private async Task ValidateTenant(Guid tenantId)
      {
            var tenant = await _tenantClient.GetTenantAsync(tenantId);

            if (tenant == null)
                  throw new UnauthorizedAccessException("Tenant not found");
      }

}