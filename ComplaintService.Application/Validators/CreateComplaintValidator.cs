using ComplaintService.Application.Dtos;
using FluentValidation;

namespace ComplaintService.Application.Validators;

public class CreateComplaintValidator : AbstractValidator<CreateComplaintDto>
{
    public CreateComplaintValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000);

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Invalid category.");

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.TenantId)
            .NotEmpty();
    }
}
