namespace ShepidiSoft.Domain.Entities.Common;

public interface IAuditEntity
{
    public DateTime Created { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? UpdatedBy { get; set; }
}