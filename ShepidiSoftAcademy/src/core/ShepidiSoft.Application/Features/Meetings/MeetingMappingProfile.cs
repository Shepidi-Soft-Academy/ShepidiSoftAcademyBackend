using AutoMapper;
using ShepidiSoft.Application.Features.Meetings.Commands.CreateMeeting;
using ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingDetail;
using ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingList;

namespace ShepidiSoft.Application.Features.Meetings;

public sealed class MeetingMappingProfile : Profile
{
    public MeetingMappingProfile()
    {
        CreateMap<CreateMeetingCommand, Meeting>(); // CreateMeetingCommand'dan Meeting'e dönüşüm
        CreateMap<Meeting, GetMeetingListQueryResponse>();
        CreateMap<Meeting, GetMeetingDetailQueryResponse>();


    }
}
