using System.ComponentModel.DataAnnotations;

namespace ComplaintService.Application.Dtos.Resolution;

public class ConfirmResolutionDto
{
    [Range(1,5)]
    public int Rating { get; set; }

    public string Feedback { get; set; } = string.Empty;
}