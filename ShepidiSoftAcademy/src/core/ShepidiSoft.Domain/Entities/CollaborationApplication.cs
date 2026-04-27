using ShepidiSoft.Domain.Entities.Common;
using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.Domain.Entities;

public sealed class CollaborationApplication : BaseEntity<int>, IAuditEntity
{

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Başvuru yapan topluluk
        public string CommunityName { get; set; } = null!;

        // Yetkili kişi bilgileri
        public string ContactName { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;
        public CollaborationApplicationStatus Status { get; set; }
        public DateTime Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get; set; }
}

