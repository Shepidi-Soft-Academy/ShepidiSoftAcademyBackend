using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Newss.Queries.GetNewsList;

public sealed record GetNewsListQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<ServiceResult<List<GetNewsListQueryResponse>>>;
