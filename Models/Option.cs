using System.ComponentModel.DataAnnotations;
namespace OnlineLearningApp.Models;

public class Option
{
    [Key]
    public int OptionId { get; set; }
    public string OptionText { get; set; } = default!;
    public bool IsCorrect { get; set; }
    
    // Navigation properties
    public int QuestionId { get; set; }
    public virtual Question Question { get; set; } = default!;
}