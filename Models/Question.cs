using System.ComponentModel.DataAnnotations;

namespace OnlineLearningApp.Models;

public class Question
{
    [Key]
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = default!;
    public string QuestionType { get; set; } = default!; // e.g., Multiple Choice, True/False
    
    // Navigation properties
    public int QuizId { get; set; }
    public virtual Quiz Quiz { get; set; } = default!;
    public virtual ICollection<Option> Options { get; set; } = default!;
}