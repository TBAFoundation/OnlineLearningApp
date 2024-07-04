using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningApp.Models;

public class Question
{
    [Key]
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = default!;
    public string QuestionType { get; set; } = default!;
    public int QuizId { get; set; }
    [ForeignKey("QuizId")]
    public virtual Quiz Quiz { get; set; } = default!;
    // Navigation properties
    public virtual ICollection<Option> Options { get; set; } = new List<Option>();
}