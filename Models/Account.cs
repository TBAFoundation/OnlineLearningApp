using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearningApp.Models;

public class Account : IdentityUser
{
    [Key]
    public string UserId { get; set; } = default!;
    [Required]
    [StringLength(100)]
    public string Username { get; set; } = default!;
    [Required]
    [StringLength(255)]
    public string PasswordHash { get; set; }
    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = default!;
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = default!;
    [Required]
    [StringLength(50)]
    public string Role { get; set; } = default!;
    // Navigation properties
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>(); // Courses taught by the instructor
    public List<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>(); // Courses the student is enrolled in
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>(); // Orders made by the user
}