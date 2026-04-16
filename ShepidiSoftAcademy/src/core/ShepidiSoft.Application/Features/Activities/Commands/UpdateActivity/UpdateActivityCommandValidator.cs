using FluentValidation;

namespace ShepidiSoft.Application.Features.Activities.Commands.UpdateActivity;


public sealed class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
    public UpdateActivityCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id 0'dan büyük olmalıdır.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Başlık alanı zorunludur.")
            .MaximumLength(200)
            .WithMessage("Başlık en fazla 200 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Açıklama alanı zorunludur.")
            .MaximumLength(1000)
            .WithMessage("Açıklama en fazla 1000 karakter olabilir.");

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Tarih alanı zorunludur.")
            .GreaterThanOrEqualTo(DateTime.Now.Date)
            .WithMessage("Tarih geçmiş bir tarih olamaz.");

        RuleFor(x => x.Location)
            .NotEmpty()
            .When(x => !x.IsOnline)
            .WithMessage("Aktivite online değilse konum alanı zorunludur.")
            .MaximumLength(300)
            .WithMessage("Konum en fazla 300 karakter olabilir.");

        RuleFor(x => x.OnlineMeetingUrl)
            .NotEmpty()
            .When(x => x.IsOnline)
            .WithMessage("Aktivite online ise toplantı bağlantısı zorunludur.")
            .Must(BeAValidUrl)
            .When(x => x.IsOnline && !string.IsNullOrWhiteSpace(x.OnlineMeetingUrl))
            .WithMessage("Toplantı bağlantısı geçerli bir URL formatında olmalıdır.");
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
