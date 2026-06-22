
using ShepidiSoft.Domain.Entities.Common;
using ShepidiSoft.Domain.Entities.Quizzes;

public class Quiz : BaseEntity<int>, IAuditEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int TimeLimit { get; set; } = default!;
    public int ScoreLimit { get; set; } = default!;
    public int CategoryId { get; set; } 
    public bool IsPublish { get; set; } = false;
    public QuizCategory? Category { get; set; } 
    public ICollection<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();

    public DateTime Created { get; set; } = default!;
    public Guid? CreatedBy { get; set; } = default!;
    public DateTime? Updated { get; set; } = default!;
    public Guid? UpdatedBy { get; set; } = default!;
    public ICollection<Question>? Questions { get; set; } = default!;
}