namespace ComplaintService.Application.Dtos;

public class CreateAuditEntryDto
{
    public Guid TenantId { get; set; }        
    public Guid? UserId { get; set; }        
    public Guid? ComplaintId { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}