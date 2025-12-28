using System.Net.Http.Json;
using ComplaintService.Application.Dtos.ClientDtos;
using ComplaintService.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ComplaintService.Infrastructure.Clients;

public class TenantClient : ITenantClient
{
    private readonly HttpClient _http;

    public TenantClient(HttpClient http,IConfiguration configuration)
    {
        _http = http;
        var internalKey = "KnGiYHvj4w8WAqGvDqS3acFhPGcUyUI0OklEFiuWxxU="; 
        _http.DefaultRequestHeaders.TryAddWithoutValidation("X-INTERNAL-KEY", internalKey);
    }

    public async Task<TenantDto?> GetTenantAsync(Guid tenantId)
    {
        
        var response = await _http.GetAsync($"/api/tenant/{tenantId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TenantDto>();
    }
}