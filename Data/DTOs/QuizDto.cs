namespace OnlineLearningApp;

public class QuizDto
{
    public int QuizId { get; set; }
    public string QuizName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime DateCreated { get; set; }
    public int ModuleId { get; set; }
    public IEnumerable<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}
