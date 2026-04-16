
using FluentValidation;

namespace ShepidiSoft.Application.Features.Courses.Commands.UpdateCourse;

public sealed class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(x => x.Title)
         .NotEmpty().WithMessage("Kurs başlığı boş bırakılamaz.")
         .Length(3, 200).WithMessage("Kurs başlığı 3 ile 200 karakter arasında olmalıdır.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Kurs açıklaması boş bırakılamaz.")
            .Length(10, 3000).WithMessage("Kurs açıklaması 10 ile 3000 karakter arasında olmalıdır.");

        RuleFor(x => x.Location)
            .Must((command, location) => command.IsOnline || !string.IsNullOrWhiteSpace(location))
            .WithMessage("Yüz yüze kurslar için konum belirtilmelidir.")
            .MaximumLength(500).When(x => !string.IsNullOrWhiteSpace(x.Location))
            .WithMessage("Konum en fazla 500 karakter olabilir.");

        RuleFor(x => x.InstructorId)
            .GreaterThan(0).WithMessage("Geçerli bir eğitmen seçilmelidir.");

        RuleFor(x => x.MeetingUrl)
            .Must((command, url) => !command.IsOnline || !string.IsNullOrWhiteSpace(url))
            .WithMessage("Çevrimiçi kurslar için toplantı URL'si belirtilmelidir.")
            .Must((command, url) => command.IsOnline || string.IsNullOrWhiteSpace(url))
            .WithMessage("Yüz yüze kurslar için toplantı URL'si belirtilmemelidir.")
            .Must(BeValidUrl).When(x => !string.IsNullOrWhiteSpace(x.MeetingUrl))
            .WithMessage("Geçersiz URL formatıdır.");

        RuleFor(x => x.CoverImageUrl)
            .NotEmpty().WithMessage("Kapak görseli URL'si boş bırakılamaz.")
            .Must(BeValidUrl).WithMessage("Geçersiz URL formatıdır.");

        RuleFor(x => x.DurationInWeeks)
            .GreaterThan(0).WithMessage("Kurs süresi en az 1 hafta olmalıdır.")
            .LessThanOrEqualTo(52).WithMessage("Kurs süresi en fazla 52 hafta olabilir.");

        RuleFor(x => x.StartedDate)
            .NotEmpty().WithMessage("Kurs başlangıç tarihi boş bırakılamaz.")
            .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Kurs başlangıç tarihi bugün ya da gelecek tarih olmalıdır.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("Kurs bitiş tarihi boş bırakılamaz.")
            .GreaterThan(x => x.StartedDate).WithMessage("Kurs bitiş tarihi başlangıç tarihinden sonra olmalıdır.")
            .Must((command, endDate) => ValidateDateRange(command.StartedDate, endDate, command.DurationInWeeks))
            .WithMessage("Kurs süresi ve tarihler tutarlı olmalıdır.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Kurs durumu boş bırakılamaz.")
            .Must(BeValidStatus).WithMessage("Kurs durumu 'Active', 'Inactive' ya da 'Archived' olmalıdır.");


    }

    private static bool BeValidUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return true;

        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private static bool BeValidStatus(string? status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return false;

        var validStatuses = new[] { "Active", "Inactive", "Archived" };
        return validStatuses.Contains(status, StringComparer.OrdinalIgnoreCase);
    }

    private static bool ValidateDateRange(DateTime startDate, DateTime endDate, int durationInWeeks)
    {
        var expectedEndDate = startDate.AddDays(durationInWeeks * 7);
        var tolerance = TimeSpan.FromDays(1);
        return Math.Abs((endDate - expectedEndDate).TotalDays) <= 1;
    }
}

