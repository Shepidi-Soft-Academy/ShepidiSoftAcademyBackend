using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.ContactMessages.Commands.CreateContactMessage;

public sealed class CreateContactMessageCommandHandler(
    IContactMessageRepository contactMessageRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<CreateContactMessageCommand, ServiceResult<int>>
{
    public async Task<ServiceResult<int>> Handle(CreateContactMessageCommand request, CancellationToken cancellationToken)
    {
        var contactMessage=mapper.Map<ContactMessage>(request);

        contactMessage.SentAt=DateTime.Now;

        await contactMessageRepository.AddAsync(contactMessage);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<int>
            .Success(contactMessage.Id);
        
    }

  
}
