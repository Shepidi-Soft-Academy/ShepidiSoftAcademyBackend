using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Organizations.Queries.GetOrganizationDetail
{
    public sealed record GetOrganizationDetailQuery(int Id)
      : IRequest<ServiceResult<GetOrganizationDetailQueryResponse>>;
}
