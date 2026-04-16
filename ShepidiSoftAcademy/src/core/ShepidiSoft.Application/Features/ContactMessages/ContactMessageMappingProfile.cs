using AutoMapper;
using ShepidiSoft.Application.Features.ContactMessages.Commands.CreateContactMessage;
using ShepidiSoft.Application.Features.ContactMessages.Queries.GetContactMessagesList;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.ContactMessages;

public sealed class ContactMessageMappingProfile : Profile
{
    public ContactMessageMappingProfile()
    {
        CreateMap<CreateContactMessageCommand, ContactMessage>();
        CreateMap<ContactMessage, GetContactMessagesListQueryResponse>();


    }


}
