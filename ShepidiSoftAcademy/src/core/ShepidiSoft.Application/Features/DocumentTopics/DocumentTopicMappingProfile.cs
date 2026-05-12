using AutoMapper;
using ShepidiSoft.Application.Features.Courses.Commands.CreateCourse;
using ShepidiSoft.Application.Features.Courses.Queries.GetMyCourses;
using ShepidiSoft.Application.Features.DocumentTopics.Command.CreateDocumentTopic;
using ShepidiSoft.Application.Features.DocumentTopics.Commands.UpdateDocumentTopic;
using ShepidiSoft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.DocumentTopics
{
    public sealed class DocumentTopicMappingProfile : Profile
    {
        public DocumentTopicMappingProfile()
        {
            CreateMap<CreateDocumentTopicCommand, DocumentTopic>();
            CreateMap<UpdateDocumentTopicCommand, DocumentTopic>();




        }


    }

}
