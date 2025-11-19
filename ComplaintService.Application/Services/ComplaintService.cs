using ComplaintService.Application.Dtos;
using ComplaintService.Application.Enums;
using ComplaintService.Application.Interfaces;
using ComplaintService.Application.Mapping;

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
      
      public async Task<CreateComplaintDto?> CreateComplaintAsync(CreateComplaintDto createComplaintDto)
      {
            var complaint = ComplaintMapper.ToEntity(createComplaintDto);
            try 
            {
                  await _complaintRepository.AddAsync(complaint);
                  return ComplaintMapper.ToDto(complaint);
            }
            catch (Exception e)
            {
                  Console.WriteLine(e);
                  return null;
            }

      }
      
      public async Task <List<ComplaintDto?>> GetComplaintsAsync()
      { 
            try
            { 
                  var complaints = await _complaintRepository.GetAllAsync();
                  return complaints.Select(ComplaintMapper.ToDto).ToList();
            } 
            catch (Exception e) 
            { 
                  Console.WriteLine($"[Error] Failed to fetch complaints: {e.Message}");
                  throw; 
            }
      }

      public async Task<ComplaintDto> GetComplaintByIdAsync(Guid id)
      {
            try
            {
                  var complaint = await _complaintRepository.GetComplaintById(id);
                  return ComplaintMapper.ToDto(complaint);
            }
            catch (Exception e)
            {
                  Console.WriteLine($"[Error] Failed to fetch complaint by id ({id}): {e.Message}");
                  throw;
            }
      }

      public async Task<ComplaintDto> UpdateComplaintAsync(Guid id,UpdateComplaintDto complaintDto)
      {
            
            try
            {
                  var existing = await _complaintRepository.GetComplaintById(id);

                  if (existing == null)
                        return null;
                  
                  ApplyUpdates(existing, complaintDto);
                  _complaintRepository.UpdateComplaint();

                  await _context.SaveChangesAsync();
                  return existing;
            }
            catch (Exception e)
            {
                  Console.WriteLine($"[Error] Failed to Update complaint by id ({id}): {e.Message}");
                  throw;
            }
      }

      public Task<DeleteStatus> DeleteComplaintAsync(Guid id)
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

      public static void ApplyUpdates(Complaint entity, UpdateComplaintDto dto)
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