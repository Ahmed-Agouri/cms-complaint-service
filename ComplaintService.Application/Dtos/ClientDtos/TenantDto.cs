namespace ComplaintService.Application.Dtos.ClientDtos;

public class TenantDto
{
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Status { get; set; } = "Active";
}