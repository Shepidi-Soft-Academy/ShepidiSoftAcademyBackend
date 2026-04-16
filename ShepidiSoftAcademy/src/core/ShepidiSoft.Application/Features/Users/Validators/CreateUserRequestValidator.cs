using FluentValidation;
using ShepidiSoft.Application.Features.Users.Dtos;

namespace ShepidiSoft.Application.Features.Users.Validators;

public sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad boş bırakılamaz.")
            .Length(2, 100).WithMessage("Ad 2 ile 100 karakter arasında olmalıdır.")
            .Matches(@"^[a-zA-ZçğıöşüÇĞİÖŞÜ\s-]+$").WithMessage("Ad sadece harfler, boşluk ve tire içerebilir.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyadı boş bırakılamaz.")
            .Length(2, 100).WithMessage("Soyadı 2 ile 100 karakter arasında olmalıdır.")
            .Matches(@"^[a-zA-ZçğıöşüÇĞİÖŞÜ\s-]+$").WithMessage("Soyadı sadece harfler, boşluk ve tire içerebilir.");

   

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

   
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.")
            .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz (10-15 rakam).");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Doğum tarihi boş bırakılamaz.")
            .LessThan(DateTime.UtcNow).WithMessage("Doğum tarihi bugünden önce olmalıdır.")
            .Must(BeValidAge).WithMessage("Yaşınız en az 15 olmalıdır.");

        RuleFor(x => x.LinkednUrl)
            .Must(BeValidUrl).When(x => !string.IsNullOrWhiteSpace(x.LinkednUrl))
            .WithMessage("LinkedIn URL'si geçerli bir URL olmalıdır.");

        RuleFor(x => x.GithubUrl)
            .Must(BeValidUrl).When(x => !string.IsNullOrWhiteSpace(x.GithubUrl))
            .WithMessage("GitHub URL'si geçerli bir URL olmalıdır.");

        RuleFor(x => x.YoutubeUrl)
            .Must(BeValidUrl).When(x => !string.IsNullOrWhiteSpace(x.YoutubeUrl))
            .WithMessage("YouTube URL'si geçerli bir URL olmalıdır.");
    }

    private static bool BeValidAge(DateTime dateOfBirth)
    {
        var age = DateTime.UtcNow.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > DateTime.UtcNow.AddYears(-age))
            age--;

        return age >= 15;
    }

    private static bool BeValidUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return true;

        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
