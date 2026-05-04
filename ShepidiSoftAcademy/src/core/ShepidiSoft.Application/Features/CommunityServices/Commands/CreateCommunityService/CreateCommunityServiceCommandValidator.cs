using FluentValidation;

namespace ShepidiSoft.Application.Features.CommunityServices.Commands.CreateCommunityService;

public sealed class CreateCommunityServiceCommandValidator : AbstractValidator<CreateCommunityServiceCommand>
{
    public CreateCommunityServiceCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Başlık boş bırakılamaz.")
            .MaximumLength(200)
            .WithMessage("Başlık 200 karakteri geçemez.");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Açıklama zorunludur.")
            .MaximumLength(2000)
            .WithMessage("Açıklama 2000 karakteri geçemez.");
        RuleFor(x => x.ImageUrl)
            .MaximumLength(500)
            .WithMessage("Resim URL'si 500 karakteri geçemez.");
    }
}
