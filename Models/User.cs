using System.ComponentModel.DataAnnotations;

namespace OnlineLearningApp.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    
    [Display(Name = "Full Name")]
    [Required(ErrorMessage = "Full Name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 100 chars")]
    public string FullName { get; set; } = default!;

    [Display(Name = "Username")]
    [Required(ErrorMessage = "Username is required")]
    [StringLength(16, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 16 chars")]
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
    
    // Navigation properties
    public virtual ICollection<Course> Courses { get; set; } = default!;// For instructors
    public virtual ICollection<Quiz> Quizzes { get; set; } = default!;
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = default!; // For students
}