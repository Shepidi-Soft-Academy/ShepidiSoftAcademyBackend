using FluentValidation;

namespace ShepidiSoft.Application.Features.OrganizationMembers.Commands.UpdateOrganizationMember;

public sealed class UpdateOrganizationMemberCommandValidator
    : AbstractValidator<UpdateOrganizationMemberCommand>
{
    public UpdateOrganizationMemberCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("MemberId boş olamaz.");

        RuleFor(x => x.PositionIds)
            .NotEmpty()
            .WithMessage("En az 1 pozisyon seçilmelidir.")
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .WithMessage("Aynı pozisyon birden fazla eklenemez.");

        RuleForEach(x => x.PositionIds)
            .GreaterThan(0)
            .WithMessage("Geçersiz PositionId.");
    }
}