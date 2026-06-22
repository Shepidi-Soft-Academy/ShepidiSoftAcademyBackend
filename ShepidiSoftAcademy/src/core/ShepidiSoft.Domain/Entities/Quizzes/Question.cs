using ShepidiSoft.Domain.Entities.Common;

public class Question : BaseEntity<int>, IAuditEntity
{
  
    public int QuizId { get; set; } 
    public string QuestionText { get; set; } = default!;
    public string OptionA { get; set; } = default!;
    public string OptionB { get; set; } = default!;
    public string OptionC { get; set; } = default!;
    public string OptionD { get; set; } = default!;
    public string CorrectOption { get; set; } = default!;
    public int Score { get; set; } 
    public Quiz? Quiz { get; set; } 
    public DateTime Created { get; set; } = default!;
    public Guid? CreatedBy { get; set; } = default!;
    public DateTime? Updated { get; set; } = default!;
    public Guid? UpdatedBy { get; set; } = default!;
}