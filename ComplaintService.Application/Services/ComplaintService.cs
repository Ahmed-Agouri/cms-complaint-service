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
      
      public async Task<ComplaintDto?> CreateComplaintAsync(ComplaintDto complaintDto)
      {
            var complaint = ComplaintMapper.ToEntity(complaintDto);
            try
            {
                  await _complaintRepository.AddAsync(complaint);
            }
            catch (Exception e)
            {
                  Console.WriteLine(e);
                  return null;
            }

            return ComplaintMapper.ToDto(complaint);
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

      public async Task<ComplaintDto> UpdateComplaintAsync(Guid id,ComplaintDto complaintDto)
      {
            
            try
            {
                  var complaint = ComplaintMapper.ToEntity(complaintDto);
                  var updatedComplaint = await _complaintRepository.UpdateComplaint(id, complaint);
                  return ComplaintMapper.ToDto(updatedComplaint);
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




}