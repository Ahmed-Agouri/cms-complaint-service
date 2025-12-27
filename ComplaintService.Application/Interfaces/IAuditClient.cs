using ComplaintService.Application.Dtos;

namespace ComplaintService.Application.Interfaces;

public interface IAuditClient
{
    Task RecordAsync(CreateAuditEntryDto dto);
}