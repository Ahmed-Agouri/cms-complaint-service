using ComplaintService.Application.Enums;

namespace ComplaintService.Application.Dtos.ClientDtos;

public class TenantDto
{
    public Guid TenantId { get; set; }
    public string Status { get; set; }
}