using ShepidiSoft.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Application.Features.Documents.Queries
{
    public record GetDocumentQueryResponse(
        int Id, string Title, string FileUrl, string TopicName, DocumentStatus Status, DateTime PublishedAt);
}
