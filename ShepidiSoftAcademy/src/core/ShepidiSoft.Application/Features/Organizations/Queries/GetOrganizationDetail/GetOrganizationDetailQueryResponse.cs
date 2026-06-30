using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Organizations.Queries.GetOrganizationDetail;

public sealed record GetOrganizationDetailQueryResponse(
    int Id,
    string Name,
    string Email,
    string? LogoUrl,
    string? LinkedInUrl,
    string? InstagramUrl,
    string? WhatsappUrl,
    string? YoutubeUrl
);