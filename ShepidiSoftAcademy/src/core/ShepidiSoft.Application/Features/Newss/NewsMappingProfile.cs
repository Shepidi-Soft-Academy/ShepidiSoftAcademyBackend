using AutoMapper;
using ShepidiSoft.Application.Features.Newss.Commands.CreateNewss;
using ShepidiSoft.Application.Features.Newss.Queries.GetNewsDetail;
using ShepidiSoft.Application.Features.Newss.Queries.GetNewsList;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Newss;

public sealed class NewsMappingProfile : Profile
{
    public NewsMappingProfile()
    {
        CreateMap<CreateNewsCommand, News>();
        CreateMap<News, GetNewsListQueryResponse>();
        CreateMap<News, GetNewsDetailQueryResponse>();
    }
}
