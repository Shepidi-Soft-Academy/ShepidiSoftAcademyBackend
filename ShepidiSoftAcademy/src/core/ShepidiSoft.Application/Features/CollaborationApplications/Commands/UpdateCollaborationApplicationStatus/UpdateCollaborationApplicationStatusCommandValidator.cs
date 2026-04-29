using FluentValidation;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Commands.UpdateCollaborationApplicationStatus;

public sealed class UpdateCollaborationApplicationStatusCommandValidator : AbstractValidator<UpdateCollaborationApplicationStatusCommand>
{
    public UpdateCollaborationApplicationStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Geçersiz başvuru ID'si!");
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Geçersiz başvuru durumu!");

    }
}
