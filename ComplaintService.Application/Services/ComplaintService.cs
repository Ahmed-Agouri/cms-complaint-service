using ComplaintService.Application.Dtos;
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
      
      public async Task<ComplaintDto?> CreateComplaintAsync(CreateComplaintDto dto)
      {
            var complaint = ComplaintMapper.ToEntity(dto);
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

}