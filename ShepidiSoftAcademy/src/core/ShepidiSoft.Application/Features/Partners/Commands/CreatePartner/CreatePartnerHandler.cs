using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Partners.Commands.CreatePartner;

public sealed class CreatePartnerHandler(IPartnerRepository partnerRepository,IFileStorageService fileService,IUnitOfWork unitOfWork,IMapper mapper)
    : IRequestHandler<CreatePartnerCommand, ServiceResult<CreatePartnerResponse>>
{
    public async Task<ServiceResult<CreatePartnerResponse>> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
    {
             
        string logo = await fileService.SaveAsync(request.Logo, "partners", cancellationToken);
       
        var partner = mapper.Map<Partner>(request);
       
        partner.SetLogoUrl(logo);

        
        await partnerRepository.AddAsync(partner);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreatePartnerResponse>.Success(new CreatePartnerResponse(partner.Id));
    }
}
