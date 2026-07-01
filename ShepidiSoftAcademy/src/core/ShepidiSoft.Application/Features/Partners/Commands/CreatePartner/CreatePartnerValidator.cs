using FluentValidation;
using Microsoft.AspNetCore.Http;


namespace ShepidiSoft.Application.Features.Partners.Commands.CreatePartner;
public sealed class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
{
    public CreatePartnerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Partner ismi boş olamaz.")
            .MaximumLength(100).WithMessage("Partner ismi en fazla 100 karakter olabilir.");

        RuleFor(x => x.Logo)
            .NotNull().WithMessage("Partner logosu gereklidir.")
            .Must(BeAValidImage).WithMessage("Lütfen geçerli bir resim dosyası seçiniz (jpg, png, jpeg).");

        RuleFor(x => x.WebsiteUrl)
            .Must(BeAValidUrl).WithMessage("Geçerli bir Web URL adresi giriniz.")
            .When(x => !string.IsNullOrEmpty(x.WebsiteUrl));
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private bool BeAValidImage(IFormFile file)
    {
        if (file == null) return false;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(file.FileName).ToLower();
        return allowedExtensions.Contains(extension);
    }
}