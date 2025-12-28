using System.Net.Http.Json;
using ComplaintService.Application.Dtos;
using ComplaintService.Application.Interfaces;

namespace ComplaintService.Infrastructure.Clients;

public class AuditClient : IAuditClient
{
    private readonly HttpClient _httpClient;

    public AuditClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
        var internalKey = "cms-dev-internal-key-dev"; 
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-INTERNAL-KEY", internalKey);
    }

    public async Task RecordAsync(CreateAuditEntryDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/audit", dto);
    }
}