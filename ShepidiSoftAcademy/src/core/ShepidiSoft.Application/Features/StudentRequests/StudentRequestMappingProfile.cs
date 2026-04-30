using AutoMapper;
using ShepidiSoft.Application.Features.StudentRequests.Commands.CreateStudentRequest;
using ShepidiSoft.Application.Features.StudentRequests.Queries;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.StudentRequests;

public sealed class StudentRequestMappingProfile : Profile
{
    public StudentRequestMappingProfile()
    {
        CreateMap<CreateStudentRequestCommand, StudentRequest>();

        CreateMap<StudentRequest, GetStudentRequestListQueryResponse>()
            .ForMember(dest => dest.StudentRequestStatus,
                       opt => opt.MapFrom(src => src.StudentRequestStatus.ToString()));
    }
}