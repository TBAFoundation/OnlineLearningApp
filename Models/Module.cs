using System.ComponentModel.DataAnnotations;
namespace OnlineLearningApp.Models;

public class Module
{
    [Key]
    public int ModuleId { get; set; }
    public string ModuleName { get; set; } = default!;
    public string Content { get; set; } = default!;
    
    // Navigation properties
    public int CourseId { get; set; }
    public virtual Course Course { get; set; } = default!;
    public virtual ICollection<Quiz> Quizzes { get; set; } = default!;
}