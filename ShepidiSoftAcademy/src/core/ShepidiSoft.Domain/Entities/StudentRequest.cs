using ShepidiSoft.Domain.Entities.Common;
using ShepidiSoft.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepidiSoft.Domain.Entities
{
    public sealed class StudentRequest : BaseEntity<Guid>, IAuditEntity
    {
        public Guid StudentId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public StudentRequestStatus StudentRequestStatus { get; set; } = StudentRequestStatus.Bekliyor;

        public Student Student { get; set; } = default!;
        public DateTime Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get ; set ; }
    }
}
