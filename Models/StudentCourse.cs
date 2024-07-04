using System.ComponentModel.DataAnnotations;

namespace OnlineLearningApp.Models;

public class StudentCourse
{
    [Key]
        public int StudentId { get; set; }
        public virtual Account Student { get; set; } = default!; // Changed from User to Account
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = default!;
        public DateTime EnrollmentDate { get; set; } = default!;
}