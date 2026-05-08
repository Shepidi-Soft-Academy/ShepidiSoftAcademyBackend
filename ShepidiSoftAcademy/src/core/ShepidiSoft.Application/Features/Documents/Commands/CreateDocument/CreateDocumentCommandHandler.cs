using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Commands.CreateDocument
{
    public sealed class CreateDocumentCommandHandler(
        IDocumentRepository documentRepository,
         IMapper mapper,
        IDocumentTopicRepository documentTopicRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateDocumentCommand, ServiceResult<int>>
    {
        public async  Task<ServiceResult<int>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            //  İlgili konu başlığı var mı 
            var topicExists = await documentTopicRepository.AnyAsync(x => x.Id == request.DocumentTopicId);
            if (!topicExists)
                return ServiceResult<int>.Fail("Seçilen konu başlığı (Topic) sistemde bulunamadı.");

            
            var document = mapper.Map<Document>(request);

            await documentRepository.AddAsync(document);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ServiceResult<int>.Success(document.Id);
        }
    }
}

