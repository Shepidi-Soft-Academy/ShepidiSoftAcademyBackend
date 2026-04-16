using FluentValidation;

namespace ShepidiSoft.Application.Features.OrganizationPositions.Commands.CreateOrganizationPosition;

public sealed class CreateOrganizationPositionCommandValidator : AbstractValidator<CreateOrganizationPositionCommand>
{
    public CreateOrganizationPositionCommandValidator()
    {

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
            .Length(3, 50).WithMessage("Başlık 3 ile 50 karakter arasında olmalıdır.");
    }
}
