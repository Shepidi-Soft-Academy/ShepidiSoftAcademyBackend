using ShepidiSoft.Domain.Entities.Common;

namespace ShepidiSoft.Domain.Entities.Quizzes;

public class QuizAttempt : BaseEntity<int>, IAuditEntity
{
    public int QuizId { get; set; }
    public int Score { get; set; }
    public Guid StudentId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime SubmittedAt { get; set; }
    public int Duration { get; set; }
    public int TotalScore { get; set; }
    public int CorrectAnswers { get; set; }
    public int IncorrectAnswers { get; set; }
    public int Percentage { get; set; }
    public bool IsCompleted { get; set; }
    public Quiz? Quiz { get; set; }
    public Student? Student { get; set; }
    public DateTime Created { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? Updated { get; set; }
    public Guid? UpdatedBy { get; set; }
}