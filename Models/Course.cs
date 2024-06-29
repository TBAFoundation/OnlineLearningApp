using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningApp.Models;

public class Course
{
    [Key]
    public int CourseId { get; set; }
    public string CourseName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ImageURL { get; set; } = default!;
    
    // Navigation properties
    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    public int InstructorId { get; set; }
    [ForeignKey("Instructor Id")]
    public virtual User Instructor { get; set; } = default!;
}