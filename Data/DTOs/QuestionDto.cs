namespace OnlineLearningApp;

public class QuestionDto
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = default!;
    public string QuestionType { get; set; } = default!;
    public int QuizId { get; set; }
    public IEnumerable<OptionDto> Options { get; set; } = new List<OptionDto>();
}
