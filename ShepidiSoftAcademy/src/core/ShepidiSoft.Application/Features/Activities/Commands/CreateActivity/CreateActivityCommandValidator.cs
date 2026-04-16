using FluentValidation;

namespace ShepidiSoft.Application.Features.Activities.Commands.CreateActivity;

public sealed class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş bırakılamaz.")
            .Length(3, 200).WithMessage("Başlık 3 ile 200 karakter arasında olmalıdır.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş bırakılamaz.")
            .Length(10, 2000).WithMessage("Açıklama 10 ile 2000 karakter arasında olmalıdır.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Tarih boş bırakılamaz.")
            .GreaterThan(DateTime.UtcNow).WithMessage("Etkinlik tarihi gelecekte olmalıdır.");

        RuleFor(x => x.IsOnline)
            .NotNull().WithMessage("Çevrimiçi durumu belirtilmelidir.");

        RuleFor(x => x.Location)
            .Must((command, location) => !command.IsOnline || string.IsNullOrWhiteSpace(location))
            .WithMessage("Çevrimiçi etkinliklerde konum belirtilmemelidir.")
            .Must((command, location) => command.IsOnline || !string.IsNullOrWhiteSpace(location))
            .WithMessage("Yüz yüze etkinliklerde konum belirtilmelidir.")
            .MaximumLength(500).WithMessage("Konum en fazla 500 karakter olabilir.");

        RuleFor(x => x.OnlineMeetingUrl)
            .Must((command, url) => !command.IsOnline || !string.IsNullOrWhiteSpace(url))
            .WithMessage("Çevrimiçi etkinliklerde toplantı URL'si belirtilmelidir.")
            .Must((command, url) => command.IsOnline || string.IsNullOrWhiteSpace(url))
            .WithMessage("Yüz yüze etkinliklerde toplantı URL'si belirtilmemelidir.")
            .Must(BeValidUrl).When(x => !string.IsNullOrWhiteSpace(x.OnlineMeetingUrl))
            .WithMessage("Geçersiz URL formatıdır.");
    }

    private static bool BeValidUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return true;

        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
