using ShepidiSoft.Domain.Entities.Common;
using ShepidiSoft.Domain.Entities.Quizzes;

public class QuizAnswer : BaseEntity<int>, IAuditEntity
{
    public int QuizAttemptId { get; set; }
    public int QuestionId { get; set; }
    public string? SelectedOption { get; set; } 
    public bool IsCorrect { get; set; } 
    public QuizAttempt? QuizAttempt { get; set; } 
    public Question? Question { get; set; } 
    public DateTime Created { get; set; } = default!;
    public Guid? CreatedBy { get; set; } = default!;
    public DateTime? Updated { get; set; } = default!;
    public Guid? UpdatedBy { get; set; } = default!;
}