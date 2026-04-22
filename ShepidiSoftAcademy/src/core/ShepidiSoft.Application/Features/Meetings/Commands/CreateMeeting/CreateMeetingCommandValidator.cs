using FluentValidation;

namespace ShepidiSoft.Application.Features.Meetings.Commands.CreateMeeting;

public sealed class CreateMeetingCommandValidator : AbstractValidator<CreateMeetingCommand>
{
    public CreateMeetingCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Başlık zorunludur.")
            .MaximumLength(200)
            .WithMessage("Başlık 200 karakteri geçmemelidir.");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Açıklama zorunludur.")
            .MaximumLength(1000)
            .WithMessage("Açıklama 1000 karakteri geçmemelidir.");
        RuleFor(x => x.MeetingLink)
            .NotEmpty()
            .WithMessage("Toplantı bağlantısı zorunludur.")
            .Must(link => Uri.IsWellFormedUriString(link, UriKind.Absolute))
            .WithMessage("Toplantı bağlantısı geçerli bir URL olmalıdır.");
        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Başlangıç zamanı gelecekte olmalıdır.");
    }
}
