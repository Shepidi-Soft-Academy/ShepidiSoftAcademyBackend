using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.ContactMessages.Commands.DeleteContactMessage;

public sealed class DeleteContactMessageCommandHandler(
    IContactMessageRepository contactMessageRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteContactMessageCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteContactMessageCommand request, CancellationToken cancellationToken)
    {
        var contactMessage = await contactMessageRepository.GetByIdAsync(request.Id);

        if (contactMessage is null)
            return ServiceResult.Fail("Mesaj bulunamadı!", HttpStatusCode.NotFound);

        contactMessageRepository.Delete(contactMessage);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);



    }
}
