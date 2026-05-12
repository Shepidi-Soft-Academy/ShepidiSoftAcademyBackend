using AutoMapper;
using ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;
using ShepidiSoft.Application.Features.Documents.Commands.UpdateDocument;
using ShepidiSoft.Application.Features.DocumentTopics.Command.CreateDocumentTopic;
using ShepidiSoft.Application.Features.DocumentTopics.Commands.UpdateDocumentTopic;
using ShepidiSoft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents
{
    public sealed class DocumentMappingProfile : Profile
    {
        public DocumentMappingProfile()
        {
            CreateMap<CreateDocumentCommand, DocumentTopic>();
            CreateMap<UpdateDocumentCommand, DocumentTopic>();
            CreateMap<CreateDocumentCommand, Document>()
    .ForMember(dest => dest.DocumentTopicId, opt => opt.MapFrom(src => src.DocumentTopicId));




        }


    }
}
