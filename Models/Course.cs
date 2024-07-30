using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningApp.Models;

public class Course : IEntityBase
{
    [Key]
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string CourseName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public CourseCategory Category { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public string ImageURL { get; set; } = default!;


    // Navigation properties
    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    public string InstructorId { get; set; } = default!;
    [ForeignKey("InstructorId")]
    public virtual Account Instructor { get; set; } = default!;
    public ICollection<Course_Module> Courses_Modules { get; set; } = new List<Course_Module>();
}