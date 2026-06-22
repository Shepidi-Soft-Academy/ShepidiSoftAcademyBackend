using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities.Quizzes;

public class QuizCategory : BaseEntity<int>, IAuditEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime Created { get; set; } = default!;
    public Guid? CreatedBy { get; set; } = default!;
    public DateTime? Updated { get; set; } = default!;
    public Guid? UpdatedBy { get; set; } = default!;
    public ICollection<Quiz>? Quizzes { get; set; }
}
