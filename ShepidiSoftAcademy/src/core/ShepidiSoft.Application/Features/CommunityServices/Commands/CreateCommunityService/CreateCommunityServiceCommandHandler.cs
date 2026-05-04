using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.CommunityServices.Commands.CreateCommunityService;

public sealed class CreateCommunityServiceCommandHandler(
    ICommunityServiceRepository communityServiceRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<CreateCommunityServiceCommand, ServiceResult<CreateCommunityServiceCommandResponse>>
{
    public async Task<ServiceResult<CreateCommunityServiceCommandResponse>> Handle(CreateCommunityServiceCommand request, CancellationToken cancellationToken)
    {
        var communityService = mapper.Map<CommunityService>(request);

        await communityServiceRepository.AddAsync(communityService);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<CreateCommunityServiceCommandResponse>.Success(new CreateCommunityServiceCommandResponse(communityService.Id));
    }
}
