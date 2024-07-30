using System.ComponentModel.DataAnnotations;

namespace OnlineLearningApp.Models;

public class StudentCourse
{
    [Key]
    public int Id { get; set; }
    public string StudentId { get; set; } = default!;
    public Account Student { get; set; } = default!; // Changed from User to Account
    public int CourseId { get; set; }
    public Course Course { get; set; } = default!;
    public DateTime EnrollmentDate { get; set; } = default!;
}