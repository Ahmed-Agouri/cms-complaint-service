using ComplaintService.Application.Dtos.ClientDtos;

namespace ComplaintService.Application.Interfaces;

public interface ITenantClient
{
    Task<TenantDto?> GetTenantAsync(Guid tenantId);
}