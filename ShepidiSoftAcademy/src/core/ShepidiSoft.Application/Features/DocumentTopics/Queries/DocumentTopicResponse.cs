using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.DocumentTopics.Queries
{
    public sealed record DocumentTopicResponse(
    int Id,
    string Name,
    string? Description,
    DateTime Created);
}
