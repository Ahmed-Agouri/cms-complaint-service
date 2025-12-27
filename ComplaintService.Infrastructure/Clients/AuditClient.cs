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
    }

    public async Task RecordAsync(CreateAuditEntryDto dto)
    {
        await _httpClient.PostAsJsonAsync("/api/audit", dto);
    }
}