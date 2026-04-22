using AutoMapper;
using ShepidiSoft.Application.Features.Meetings.Commands.CreateMeeting;

namespace ShepidiSoft.Application.Features.Meetings;

public sealed class MeetingMappingProfile : Profile
{
    public MeetingMappingProfile()
    {
        CreateMap<CreateMeetingCommand, Meeting>(); // CreateMeetingCommand'dan Meeting'e dönüşüm

    }
}
