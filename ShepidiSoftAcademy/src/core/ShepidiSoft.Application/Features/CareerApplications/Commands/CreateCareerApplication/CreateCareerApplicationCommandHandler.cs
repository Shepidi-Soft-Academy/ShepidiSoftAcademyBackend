using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using System.Net;

namespace ShepidiSoft.Application.Features.CareerApplications.Commands.CreateCareerApplication;

public sealed class CreateCareerApplicationCommandHandler(
    ICareerApplicationRepository careerApplicationRepository,
    IOrganizationPositionRepository organizationPositionRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<CreateCareerApplicationCommand, ServiceResult<CreateCareerApplicationCommandResponse>>
{
    public async Task<ServiceResult<CreateCareerApplicationCommandResponse>> Handle(
        CreateCareerApplicationCommand request,
        CancellationToken cancellationToken)
    {
        // Fast Fail #1 
        var positionExists = await organizationPositionRepository.AnyAsync(
            x => x.Id == request.OrganizationPositionId && x.IsActive);

        if (!positionExists)
            return ServiceResult<CreateCareerApplicationCommandResponse>.Fail(
                "Başvurmak istediğiniz pozisyon mevcut değil veya kapalı.",
                HttpStatusCode.NotFound);

        // Fast Fail #2 — Tekrar eden basvuru 
        var alreadyApplied = await careerApplicationRepository.AnyAsync(
            x => x.Email == request.Email &&
                 x.OrganizationPositionId == request.OrganizationPositionId);

        if (alreadyApplied)
            return ServiceResult<CreateCareerApplicationCommandResponse>.Fail(
                "Bu pozisyon için zaten başvurdunuz.",HttpStatusCode.Conflict);

        // Map & Persist
        var entity = mapper.Map<CareerApplication>(request);

        await careerApplicationRepository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreateCareerApplicationCommandResponse>
            .Success(new CreateCareerApplicationCommandResponse(entity.Id));
    }
}