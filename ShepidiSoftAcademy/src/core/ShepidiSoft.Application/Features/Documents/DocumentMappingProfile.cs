using AutoMapper;
using ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Documents
{
    public sealed class DocumentMappingProfile : Profile
    {
        public DocumentMappingProfile()
        {
            CreateMap<CreateDocumentCommand, DocumentTopic>();
            CreateMap<CreateDocumentCommand, Document>()
    .ForMember(dest => dest.DocumentTopicId, opt => opt.MapFrom(src => src.DocumentTopicId));




        }


    }
}
