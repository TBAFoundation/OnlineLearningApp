namespace OnlineLearningApp;

public class OptionDto
{
    public int OptionId { get; set; }
    public string OptionText { get; set; } = default!;
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; }
}
