using FluentValidation;

namespace ShepidiSoft.Application.Features.Offerings.Commands.UpdateOffering;

public sealed class UpdateOfferingCommandValidator : AbstractValidator<UpdateOfferingCommand>
{
    public UpdateOfferingCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Geçerli bir Id belirtilmelidir.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
            .Length(3, 200).WithMessage("Başlık 3 ile 200 karakter arasında olmalıdır.");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("Aktif durumu belirtilmelidir.");
    }
}
