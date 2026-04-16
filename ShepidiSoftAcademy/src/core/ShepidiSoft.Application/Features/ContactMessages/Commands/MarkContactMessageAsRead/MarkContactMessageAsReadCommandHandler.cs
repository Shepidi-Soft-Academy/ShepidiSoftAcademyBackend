using MediatR;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.ContactMessages.Commands.MarkContactMessageAsRead;
using System.Net;

public sealed class MarkContactMessageAsReadCommandHandler : IRequestHandler<MarkContactMessageAsReadCommand, ServiceResult>
{
    private readonly IContactMessageRepository _contactMessageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkContactMessageAsReadCommandHandler(IContactMessageRepository contactMessageRepository,IUnitOfWork unitOfWork)
    {
        _contactMessageRepository = contactMessageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult> Handle(MarkContactMessageAsReadCommand request, CancellationToken cancellationToken)
    {
        
        var message = await _contactMessageRepository.GetByIdAsync(request.Id);
        if (message == null)
            return ServiceResult.Fail("Mesaj bulunamadı.",HttpStatusCode.NotFound);

        message.IsRead = true;

         _contactMessageRepository.Update(message);
        await  _unitOfWork.SaveChangesAsync(cancellationToken);
    

        return ServiceResult.Success();
    }
}