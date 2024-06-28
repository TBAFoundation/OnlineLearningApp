using System.ComponentModel.DataAnnotations;

namespace OnlineLearningApp.Models;

public class StudentCourse
{
    [Key]
    public int StudentId { get; set; }
    public virtual User Student { get; set; } = default!;
    
    public int CourseId { get; set; }
    public virtual Course Course { get; set; } = default!;
    
    public DateTime EnrollmentDate { get; set; } = default!;
}