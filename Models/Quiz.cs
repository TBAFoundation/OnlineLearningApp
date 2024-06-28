using System.ComponentModel.DataAnnotations;

namespace OnlineLearningApp.Models;

public class Quiz
{
    [Key]
    public int QuizId { get; set; }
    public string QuizName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime DateCreated { get; set; }
    
    // Navigation properties
    public int ModuleId { get; set; }
    public virtual Module Module { get; set; } = default!;
    public virtual ICollection<Question> Questions { get; set; } = default!;
}