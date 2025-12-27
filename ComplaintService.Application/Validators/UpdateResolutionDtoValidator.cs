using ComplaintService.Application.Dtos.Resolution;

namespace ComplaintService.Application.Validators;

using FluentValidation;

public class UpdateResolutionDtoValidator : AbstractValidator<UpdateResolutionDto>
{
    public UpdateResolutionDtoValidator()
    {
        RuleFor(x => x.ResolutionNotes)
            .MaximumLength(4000);
    }
}