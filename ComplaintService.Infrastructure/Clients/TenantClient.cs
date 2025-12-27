using System.Net;
using System.Net.Http.Json;
using ComplaintService.Application.Dtos.ClientDtos;
using ComplaintService.Application.Interfaces;

namespace ComplaintService.Infrastructure.Clients;

public class TenantClient : ITenantClient
{
    private readonly HttpClient _http;

    public TenantClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<TenantDto?> GetTenantAsync(Guid tenantId)
    {
        var response = await _http.GetAsync($"/api/tenant/{tenantId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TenantDto>();
    }

    public async Task<bool> ValidateTenantAsync(Guid tenantId)
    {
        var tenant = await GetTenantAsync(tenantId);

        if (tenant == null)
            return false;

        return string.Equals(tenant.Status, "Active", StringComparison.OrdinalIgnoreCase);
    }
}