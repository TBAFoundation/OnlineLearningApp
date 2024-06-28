using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningApp.Models;

public class Course
{
    [Key]
    public int CourseId { get; set; }
    public string CourseName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Duration { get; set; } = default!;
    
    // Navigation properties
    public int InstructorId { get; set; }
    [ForeignKey("Instructor Id")]
    public virtual User Instructor { get; set; } = default!;
    public virtual ICollection<Module> Modules { get; set; } = default!;
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = default!;
}